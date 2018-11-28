using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CryptoApp.Classes;
using CryptoApp.CloudServiceReference;
using CryptoLib;

namespace CryptoApp.Forms
{

    // Progress delegate to avoid cross-thread operation errors
    public delegate void ProgressDelegate(int value);

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

        // Thread as a field so it can be aborted via cancel
        private Thread _thread;
        
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
                ControlBox = false;

                // Set window title
                Text = _upload ? "File Upload" : "File Download";
                
                // Set label
                lblStatus.Text = (_upload ? "Uploading " : "Downloading ") + Path.GetFileName(_localFilePath);

                // Set thread method
                _thread = _upload ? new Thread(Upload) : new Thread(Download);

                // Start thread
                _thread.Start();
            }
            catch (Exception exception)
            {
                throw new ArgumentException(exception.Message);
            }
        }

        private void Upload()
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

                // Close the form
                Invoke(new MethodInvoker(Close));
            }
        }

        private void Download()
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
                            progressBar.Invoke(new ProgressDelegate(UpdateProgress), 100);
                        else
                            progressBar.Invoke(new ProgressDelegate(UpdateProgress), 
                                (int)(writeStream.Position * 100 / length));

                    } while (!lastChunk);

                    writeStream.Close();
                }
                
                // Deallocate stream
                inputStream.Dispose();
                cloudProxy.Close();
            }
            catch (Exception exception)
            {
                throw new ArgumentException(exception.Message);
            }
            finally
            {
                // Close service proxy
                cloudProxy.Close();

                // Close the form using invoke to avoid cross-thread operation errors
                Invoke(new MethodInvoker(Close));
            }
        }

        #endregion

        #region Delegate Methods
        

        private void uploadStream_ProgressChanged(object sender, ProgressStream.ProgressChangedEventArgs e)
        {
            if (e.Length == 0) return;
            progressBar.Invoke(new ProgressDelegate(UpdateProgress), (int)(e.BytesRead * 100 / e.Length));
        }

        private void UpdateProgress(int value)
        {
            // Counteracting animation lag
            if (value == progressBar.Maximum)
            {
                progressBar.Maximum = value + 1;
                progressBar.Value = value + 1;
                progressBar.Maximum = value;
            }
            else
                progressBar.Value = value + 1;

            lblProgress.Text = value + "% Done";
        }

        #endregion

    }
}
