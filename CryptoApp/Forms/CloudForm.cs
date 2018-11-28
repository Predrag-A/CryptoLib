using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CryptoApp.CloudServiceReference;

namespace CryptoApp.Forms
{
    public partial class CloudForm : Form
    {

        #region Fields
        
        private List<string> _fileList;
        private BindingSource _bindingSource;
        private bool _expandLog;

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

                // Logging
                Log("Connected to cloud, " + _fileList.Count + " files available");

                // Setting data source for the listbox control
                _bindingSource.DataSource = _fileList;
                fileListBox.DataSource = _bindingSource;
            }
            catch (Exception e)
            {
                Log(e.Message);
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
                Log("Uploading " + Path.GetFileName(localFilePath));

                udf.Show();

                //Log("File " + Path.GetFileName(localFilePath) + " successfully uploaded");
                
            }
            catch (Exception exception)
            {
                Log(exception.Message);
            }
        }

        private void Download(string cloudFileName, string localFilePath)
        {
            
            try
            {
                var udf = new UploadDownloadForm(_fileList, _bindingSource, localFilePath, cloudFileName);
                Log("Downloading " + cloudFileName);

                udf.Show();

                //Log("File " + cloudFileName + " successfully downloaded");
                
            }
            catch (Exception exception)
            {
                Log(exception.Message);
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
                    Log("File " + cloudFileName + " deleted");
                    _fileList.RemoveAt(fileListBox.SelectedIndex);
                    _bindingSource.ResetBindings(false);
                }
                else
                {
                    Log("Error deleting the file");
                }
            }
            catch (Exception exception)
            {
                Log(exception.Message);
            }
            finally
            {
                // Close service proxy
                cloudProxy.Close();
            }
        }

        private void Log(string msg)
        {
            txtLog.Text = DateTime.Now.ToString("T") + "  " + msg + "\r\n" + txtLog.Text;
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

            // Getting file name
            var fileName = fileListBox.SelectedItem.ToString();

            // Delete the file
            Delete(fileName);
        }
        
        private void btnExpand_Click(object sender, EventArgs e)
        {
            _expandLog = !_expandLog;
            if (_expandLog)
            {
                // Change text log size
                txtLog.ScrollBars = ScrollBars.Vertical;
                txtLog.Size = new Size(txtLog.Size.Width, txtLog.Size.Height + 100);

                // Enable
                txtLog.Enabled = true;
            }
            else
            {
                // Change text log size
                txtLog.ScrollBars = ScrollBars.None;
                txtLog.Size = new Size(txtLog.Size.Width, txtLog.Size.Height - 100);

                // Disable
                txtLog.Enabled = false;
            }
        }

        #endregion
    }
}
