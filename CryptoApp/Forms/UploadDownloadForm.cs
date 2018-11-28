using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CryptoApp.Classes;
using CryptoApp.CloudServiceReference;
using CryptoLib;

namespace CryptoApp.Forms
{
    public partial class UploadDownloadForm : Form
    {

        #region Fields
        
        // True = Upload; False = Download
        private readonly bool _upload;

        // Reference to file list and bindingsource
        private readonly List<string> _fileList;
        private readonly BindingSource _bindingSource;

        // File variables
        private readonly string _localFilePath;
        private string _cloudFileName;

        // Encrypted data chunk size
        private const int ChunkSize = 2056;
        
        #endregion

        #region Constructors
        
        // Constructor for uploading 
        public UploadDownloadForm(List<string> fileList, BindingSource bindingSource, string localFilePath)
        {
            // Initializing variables
            _upload = true;
            _fileList = fileList;
            _bindingSource = bindingSource;
            _localFilePath = localFilePath;

            InitializeComponent();
            Initialize();
        }

        // Constructor for downloading
        public UploadDownloadForm(List<string> fileList, BindingSource bindingSource,
            string localFilePath, string cloudFileName)
        {
            // Initializing variables
            _upload = false;
            _fileList = fileList;
            _bindingSource = bindingSource;
            _localFilePath = localFilePath;
            _cloudFileName = cloudFileName;

            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            try
            {
                // Set window title
                Text = _upload ? "File Upload" : "File Download";
                
                // Set label
                lblStatus.Text = (_upload ? "Uploading " : "Downloading ") + Path.GetFileName(_localFilePath);

                // Start backgroundWorker
                backgroundWorker.RunWorkerAsync();
            }
            catch (Exception exception)
            {
                throw new ArgumentException(exception.Message);
            }
        }

        private void uploadStream_ProgressChanged(object sender, ProgressStream.ProgressChangedEventArgs e)
        {
            if (e.Length == 0) return;
            backgroundWorker.ReportProgress((int)(e.BytesRead * 100 / e.Length));
        }

        #region Upload

        private void Upload(System.ComponentModel.DoWorkEventArgs e)
        {

            // Initializing service proxy
            var cloudProxy = new CloudServiceClient();

            try
            {
                // Creating fileInfo and fileName
                var fileInfo = new FileInfo(_localFilePath);
                var fileName = fileInfo.Name;

                // Uploading file
                using (var stream = new FileStream(_localFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (var uploadStream = new ProgressStream(stream))
                    {
                        // Adding event handler to uploadStream
                        uploadStream.ProgressChanged += uploadStream_ProgressChanged;

                        // Invoking service method
                        cloudProxy.UploadFile(ref fileName, fileInfo.Length, uploadStream);

                        // Check for cancellation
                        if (backgroundWorker.CancellationPending)
                        {
                            e.Cancel = true;
                            _cloudFileName = fileName;
                            return;
                        }

                        // Adding file name to list instead of invoking GetFileList
                        _fileList.Add(fileName);
                        _bindingSource.ResetBindings(false);

                    }
                }
            }
            catch (Exception exception)
            {
                throw new ArgumentException(exception.Message);
            }
            finally
            {
                // Closing service proxy
                cloudProxy.Close();
            }
        }

        #endregion

        #region Download

        private void Download(System.ComponentModel.DoWorkEventArgs e)
        {

            // Initializing service proxy
            var cloudProxy = new CloudServiceClient();

            try
            {

                // Initializing client-side decryption
                var clientCypher = new XTEA();

                // Getting decryption key
                clientCypher.SetKey(Encoding.ASCII.GetBytes(cloudProxy.GetKey()));

                // Initializing stream from service
                var length = cloudProxy.DownloadFile(ref _cloudFileName, out var inputStream);

                // Write stream to disk
                using (var writeStream = new FileStream(_localFilePath, FileMode.CreateNew, FileAccess.Write))
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
                        var decryptedBuffer = clientCypher.Decrypt(buffer);

                        // Write bytes to output stream
                        writeStream.Write(decryptedBuffer, 0, decryptedBuffer.Length);

                        // Update progress bar
                        if (lastChunk)
                            backgroundWorker.ReportProgress(100);
                        else
                            backgroundWorker.ReportProgress((int)(writeStream.Position * 100 / length));

                        // Check for cancellation
                        if (!backgroundWorker.CancellationPending) continue;

                        e.Cancel = true;
                        inputStream.Dispose();
                        return;

                    } while (!lastChunk);

                    writeStream.Close();
                }
                
                // Deallocate stream
                inputStream.Dispose();
            }
            catch (Exception exception)
            {
                throw new ArgumentException(exception.Message);
            }
            finally
            {
                // Close service proxy
                cloudProxy.Close();
            }
        }

        #endregion
        

        #endregion

        #region Backgroundworker Methods

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // Invoke method depending on bool value
            if(_upload)
                Upload(e);
            else
                Download(e);
        }
        

        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            // Update progress bar
            if (e.ProgressPercentage == progressBar.Maximum)
            {
                // Counteracting animation lag
                progressBar.Maximum = e.ProgressPercentage + 1;
                progressBar.Value = e.ProgressPercentage + 1;
                progressBar.Maximum = e.ProgressPercentage;
            }
            else
                progressBar.Value = e.ProgressPercentage + 1;
                
            // Update progress label
            lblProgress.Text = e.ProgressPercentage + "% Done";
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            // Operation was cancelled
            if (e.Cancelled)
            {
                // If uploading delete file server-side
                if (_upload)
                {
                    // Initializing service proxy
                    var cloudProxy = new CloudServiceClient();

                    // Deleting file server-side
                    cloudProxy.DeleteFile(_cloudFileName);

                    // Closing service proxy
                    cloudProxy.Close();

                }
                // If downloading delete file locally
                else
                {
                    if(File.Exists(_localFilePath))
                        File.Delete(_localFilePath);
                }
                
            }
            // Error
            else if (e.Error != null)
            {
                Close();
                MessageBox.Show("There has been an error in the process: " + e.Error.Message, 
                    "Error", MessageBoxButtons.OK);
            }
            // Operation completed
            else
            {
                // Close form when work completes
                Close();
            }
        }

        #endregion


        #region Form Methods

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Close form
            Close();
        }

        private void UploadDownloadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Cancel backgroundworker if it is not completed
            if (backgroundWorker.IsBusy)
                backgroundWorker.CancelAsync();
        }

        #endregion
    }
}
