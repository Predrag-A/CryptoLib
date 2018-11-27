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

        private readonly ICryptoService _proxy;
        private List<string> _fileList;
        private BindingSource _bindingSource;

        #endregion

        #region Constructors

        public CloudForm(ICryptoService p)
        {
            _proxy = p;
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _bindingSource = new BindingSource();

            try
            {

                var cloudProxy = new CloudServiceClient();

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
        }

        #endregion

        #region Form Methods

        private void btnUpload_Click(object sender, EventArgs e)
        {
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

                        // Initializing service proxy
                        var cloudProxy = new CloudServiceClient();
                        
                        cloudProxy.UploadFile(ref fileName, fileInfo.Length, stream);

                        // Adding file name to list instead of invoking GetFileList
                        _fileList.Add(fileName);
                        _bindingSource.ResetBindings(false);

                        lblStatus.Text = "Status: File successfully uploaded";

                        // Closing service
                        cloudProxy.Close();
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
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {

                // Return if no files are available
                if (_fileList.Count == 0) return;

                // Getting folder path for storing the downloaded file
                if (fbd.ShowDialog() != DialogResult.OK) return;

                // Initializing service proxy
                var cloudProxy = new CloudServiceClient();

                // Initializing client-side decryption
                var ClientCypher = new XTEA();

                // Getting decryption key
                ClientCypher.SetKey(Encoding.ASCII.GetBytes(cloudProxy.GetKey()));

                // Getting stream from the service
                var fileName = fileListBox.SelectedItem.ToString();
                Stream inputStream;
                long length = cloudProxy.DownloadFile(ref fileName, out inputStream);

                // Storing file locally
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
                    int chunkSize = 2048;
                    byte[] buffer = new byte[chunkSize];

                    do
                    {
                        // Read bytes from input stream
                        int bytesRead = inputStream.Read(buffer, 0, chunkSize);
                        if (bytesRead == 0) break;


                        var decryptedBuffer = ClientCypher.Decrypt(buffer);
                        // Write bytes to output stream
                        writeStream.Write(decryptedBuffer, 0, decryptedBuffer.Length);

                        // Update progress bar
                        progressBar.Value = (int) (writeStream.Position * 100 / length);
                    } while (true);

                    writeStream.Close();
                }

                // Close service proxy and deallocate stream
                inputStream.Dispose();
                cloudProxy.Close();


                /*
                // Old Method
                // Return if no files are available
                if (_fileList.Count == 0) return;

                // Getting folder path for storing the downloaded file
                if (fbd.ShowDialog() != DialogResult.OK) return;
                
                // Getting result from the service
                var fileName = fileListBox.SelectedItem.ToString();
                var result = _proxy.DownloadFile(fileName);

                // Storing file locally
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

                File.WriteAllText(newFullPath, Encoding.ASCII.GetString(result));

                lblStatus.Text = "Status: File successfully downloaded";
                */

            }
            catch (Exception exception)
            {
                lblStatus.Text = "Status: " + exception.Message;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
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
                if (_proxy.DeleteFile(fileName))
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
        }

        void uploadStream_ProgressChanged(object sender, ProgressStream.ProgressChangedEventArgs e)
        {
            if (e.Length != 0)
                progressBar.Value = (int)(e.BytesRead * 100 / e.Length);
        }

        #endregion

    }
}
