using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using CryptoApp.CloudServiceReference;

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
                // Getting file names from cloud server
                _fileList = new List<string>(cloudProxy.GetFileList().Select(Path.GetFileName));

                // Setting data source for the listbox control
                _bindingSource.DataSource = _fileList;
                fileListBox.DataSource = _bindingSource;
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

        private void Upload(string localFilePath)
        {
            try
            {
                var udf = new UploadDownloadForm(_fileList, _bindingSource, localFilePath);
                udf.Show();

                lblStatus.Text = "Status: File " + Path.GetFileName(localFilePath) + " successfully uploaded";
                

            }
            catch (Exception exception)
            {
                lblStatus.Text = "Status: " + exception.Message;
            }
        }

        private void Download(string cloudFileName, string localFilePath)
        {
            
            try
            {
                var udf = new UploadDownloadForm(_fileList, _bindingSource, localFilePath, cloudFileName);
                udf.Show();

                lblStatus.Text = "Status: File " + cloudFileName + " successfully downloaded";
                
            }
            catch (Exception exception)
            {
                lblStatus.Text = "Status: " + exception.Message;
            }
        }

        private void Delete(string cloudFileName)
        {
            // Initializing service proxy
            var cloudProxy = new CloudServiceClient();

            try
            {
                if (cloudProxy.DeleteFile(cloudFileName))
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

        #endregion

        #region Form Methods

        private void btnUpload_Click(object sender, EventArgs e)
        {
            // Getting file name from open file dialog
            if (ofd.ShowDialog() != DialogResult.OK) return;
            
            // Invoke upload method
            Upload(ofd.FileName);
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            // Return if no files are available
            if (_fileList.Count == 0) return;

            // Getting folder path for storing the downloaded file
            if (fbd.ShowDialog() != DialogResult.OK) return;

            // Getting file name to download from listbox control
            var fileName = fileListBox.SelectedItem.ToString();


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

            // Invoke download method
            Download(fileName, newFullPath);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Return if no files are available
            if (_fileList.Count == 0) return;

            // Validating request
            if (DialogResult.Yes !=
                MessageBox.Show("Are you sure you want to delete the file from the server?",
                    "Warning", MessageBoxButtons.YesNo)) return;

            // Deleting file on the service
            var fileName = fileListBox.SelectedItem.ToString();

            // Delete the file
            Delete(fileName);
        }

        #endregion

    }
}
