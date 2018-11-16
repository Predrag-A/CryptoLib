﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CryptoApp.CryptoServiceReference;

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
                // Getting only file names
                _fileList = new List<string>(_proxy.GetFileList().Select(Path.GetFileName));
                _bindingSource.DataSource = _fileList;
                fileListBox.DataSource = _bindingSource;
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
            try
            {
                // Getting file name for upload
                if (ofd.ShowDialog() != DialogResult.OK) return;
                var result = _proxy.UploadFile(ofd.FileName);

                // Adding file name to list instead of invoking GetFileList
                if (result > 0)
                {
                    var extension = Path.GetExtension(ofd.FileName);
                    var name = Path.GetFileNameWithoutExtension(ofd.FileName);
                    _fileList.Add(name + "(" + result + ")" + extension);
                }
                else
                    _fileList.Add(Path.GetFileName(ofd.FileName));

                _bindingSource.ResetBindings(false);
                lblStatus.Text = "Status: File successfully uploaded";
            }
            catch (Exception exception)
            {
                lblStatus.Text = "Status: " + exception.Message;
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
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

            }
            catch (Exception exception)
            {
                lblStatus.Text = "Status: " + exception.Message;
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

        #endregion

    }
}
