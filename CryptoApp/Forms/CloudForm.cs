using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CryptoApp.Classes;
using CryptoApp.CloudServiceReference;
using CryptoApp.CryptoServiceReference;
using CryptoLib;

namespace CryptoApp.Forms
{
    public partial class CloudForm : Form
    {

        #region Fields
        
        private List<string> _fileList;
        private BindingSource _bindingSource;
        private const int ChunkSize = 2056;

        #endregion

        #region Constructors

        public CloudForm()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _bindingSource = new BindingSource();

            // Initializing service proxy
            var cloudProxy = new CloudServiceClient();

            try
            {
                // Getting only file names
                _fileList = new List<string>(cloudProxy.GetFileList().Select(Path.GetFileName));
                _bindingSource.DataSource = _fileList;
                fileListBox.DataSource = _bindingSource;

                cloudProxy.Close();
            }
            catch (Exception e)
            {
                lblStatus.Text = "Status: " + e.Message;
            }
            finally
            {
                // Close service proxy
                cloudProxy.Close();
            }
        }

        #endregion

        #region Form Methods

        private void btnUpload_Click(object sender, EventArgs e)
        {
            // Initializing service proxy
            var cloudProxy = new CloudServiceClient();

            Cursor = Cursors.WaitCursor;
            try
            {
                // Getting file name for upload
                if (ofd.ShowDialog() != DialogResult.OK) return;

                var fileInfo = new FileInfo(ofd.FileName);
                var fileName = fileInfo.Name;

                // Uploading file
                using (var stream = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                {
                    using (var uploadStream = new ProgressStream(stream))
                    {
                        // Adding event handler
                        uploadStream.ProgressChanged += uploadStream_ProgressChanged;
                        
                        cloudProxy.UploadFile(ref fileName, fileInfo.Length, stream);

                        // Adding file name to list instead of invoking GetFileList
                        _fileList.Add(fileName);
                        _bindingSource.ResetBindings(false);

                        lblStatus.Text = "Status: File " + fileName + " successfully uploaded";
                    }
                }
                
            }
            catch (Exception exception)
            {
                lblStatus.Text = "Status: " + exception.Message;
            }
            finally
            {
                Cursor = Cursors.Default;

                // Closing service proxy
                cloudProxy.Close();
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Initializing service proxy
            var cloudProxy = new CloudServiceClient();

            try
            {

                // Return if no files are available
                if (_fileList.Count == 0) return;

                // Getting folder path for storing the downloaded file
                if (fbd.ShowDialog() != DialogResult.OK) return;

                // Initializing client-side decryption
                var ClientCypher = new XTEA();

                // Getting decryption key
                 ClientCypher.SetKey(Encoding.ASCII.GetBytes(cloudProxy.GetKey()));

                // Getting stream from the service
                var fileName = fileListBox.SelectedItem.ToString();
                Stream inputStream;
                long length = cloudProxy.DownloadFile(ref fileName, out inputStream);

                // Creating local file name
                var name = Path.GetFileNameWithoutExtension(fileName);
                var extension = Path.GetExtension(fileName);
                var fullPath = fbd.SelectedPath + "//" + fileName;
                var newFullPath = fullPath;
                var count = 1;

                // Check if file exists as to not overwrite file
                while (File.Exists(newFullPath))
                {
                    count++;
                    newFullPath = fbd.SelectedPath + "//" + name + "(" + count + ")" + extension;
                }

                // Write stream to disk
                using (var writeStream = new FileStream(newFullPath, FileMode.CreateNew, FileAccess.Write))
                {
                    // Initializing buffer
                    var buffer = new byte[ChunkSize];

                    // Initializing boolean determining whether it's the last chunk
                    var lastChunk = false;

                    do
                    {
                        // Read bytes from input stream
                        var bytesRead = inputStream.Read(buffer, 0, ChunkSize);
                        if (bytesRead == 0) break;

                        // Check if it is the last chunk
                        if (bytesRead < ChunkSize)
                        {
                            var temp = new byte[bytesRead];
                            Array.Copy(buffer, temp, bytesRead);
                            buffer = temp;
                            lastChunk = true;
                        }

                        // Decrypt data from the buffer
                        var decryptedBuffer = ClientCypher.Decrypt(buffer);

                        // Write bytes to output stream
                        writeStream.Write(decryptedBuffer, 0, decryptedBuffer.Length);

                        // Update progress bar
                        if (lastChunk)
                            progressBar.Value = 100;
                        else
                            progressBar.Value = (int) (writeStream.Position * 100 / length);
                        
                    } while (!lastChunk);

                    writeStream.Close();
                }


                lblStatus.Text = "Status: File " + fileName + " successfully downloaded";

                // Deallocate stream
                inputStream.Dispose();
                cloudProxy.Close();

            }
            catch (Exception exception)
            {
                lblStatus.Text = "Status: " + exception.Message;
            }
            finally
            {
                Cursor = Cursors.Default;

                // Close service proxy
                cloudProxy.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Initializing service proxy
            var cloudProxy = new CloudServiceClient();
            try
            {

                // Return if no files are available
                if (_fileList.Count == 0) return;

                // Validating request
                if (DialogResult.Yes !=
                    MessageBox.Show("Are you sure you want to delete the file from the server?",
                        "Warning", MessageBoxButtons.YesNo)) return;

                // Deleting file on the service
                var fileName = fileListBox.SelectedItem.ToString();
                if (cloudProxy.DeleteFile(fileName))
                {
                    lblStatus.Text = "Status: File successfully deleted";
                    _fileList.RemoveAt(fileListBox.SelectedIndex);
                    _bindingSource.ResetBindings(false);
                }
                else
                {
                    lblStatus.Text = "Status: Error deleting the file";
                }
            }
            catch (Exception exception)
            {
                lblStatus.Text = "Status: " + exception.Message;
            }
            finally
            {
                // Close service proxy
                cloudProxy.Close();
            }
        }

        private void uploadStream_ProgressChanged(object sender, ProgressStream.ProgressChangedEventArgs e)
        {
            if (e.Length != 0)
                progressBar.Value = (int)(e.BytesRead * 100 / e.Length);
        }

        #endregion

    }
}
