using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using CryptoApp.Classes;
using CryptoApp.CryptoServiceReference;
using CryptoApp.Forms;
using CryptoLib;
using Encoding = System.Text.Encoding;

namespace CryptoApp
{
    public partial class MainForm : Form
    {

        #region Fields

        private readonly ICryptoService _proxy = new CryptoServiceClient();
        private FileSystemWatcher _watcher;
        private FileProcessor _processor;

        #endregion

        #region Constructors

        public MainForm()
        {
            InitializeComponent();

            Settings.Instance.Load("settings.xml");
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            fbdInput.SelectedPath = Settings.Instance.FswInput;
            fSWInputFolderToolStripMenuItem.ToolTipText = Settings.Instance.FswInput;

            fbdOutput.SelectedPath = Settings.Instance.FswOutput;
            fSWOutputFolderToolStripMenuItem.ToolTipText = Settings.Instance.FswOutput;

            fSWOnOffToolStripMenuItem.Checked = Settings.Instance.FswEnabled;

            CheckAlgo(Settings.Instance.Algo);

            encryptToolStripMenuItem.Checked = !Settings.Instance.FswDecrypt;
            decryptToolStripMenuItem.Checked = Settings.Instance.FswDecrypt;

            // Initialize FSW
            _watcher = new FileSystemWatcher
            {
                Path = Settings.Instance.FswInput,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName |
                               NotifyFilters.DirectoryName,
                IncludeSubdirectories = false
            };

            // Adding event handlers
            _watcher.Created += new FileSystemEventHandler(watcher_Created);

            // Turning on or off
            _watcher.EnableRaisingEvents = Settings.Instance.FswEnabled;

            // Initialize File Processor
            _processor = new FileProcessor();

            // Process unprocessed files if any, if FSW is enabled
            if (Settings.Instance.FswEnabled) GetFiles();
            
        }
        
        // Checks if the textbox is empty
        private bool IsEmpty(TextBox t)
        {
            if (!string.IsNullOrEmpty(t.Text)) return false;
            MessageBox.Show("Fill out the text that you want to encrypt or decrypt.");
            return true;
        }

        private void CheckAlgo(Algorithm a)
        {
            switch (a)
            {
                case Algorithm.DoubleTranposition:
                    doubleTranspositionToolStripMenuItem.Checked = true;
                    break;

                case Algorithm.XTEA:
                    xTEAToolStripMenuItem.Checked = true;
                    break;

                case Algorithm.MD5:
                    decryptBtn.Enabled = false;
                    mD5ToolStripMenuItem.Checked = true;
                    break;

                case Algorithm.Knapsack:
                    knapsackToolStripMenuItem.Checked = true;
                    break;
            }
        }

        private void UncheckAlgo()
        {
            doubleTranspositionToolStripMenuItem.Checked = false;
            xTEAToolStripMenuItem.Checked = false;
            mD5ToolStripMenuItem.Checked = false;
            knapsackToolStripMenuItem.Checked = false;
            decryptBtn.Enabled = true;
        }

        // Sets the algorithm type in the Settings instance
        private void SetAlgo(Algorithm a)
        {
            if (Settings.Instance.Algo == a)
                return;
            Settings.Instance.Algo = a;
            UncheckAlgo();
            CheckAlgo(a);
        }

        // Sets keys and properties from the Settings on launch
        private void SetKeys()
        {
            try
            {
                // Set Double Transposition Key
                var DTKey = Settings.Instance.DTColKey + "," + Settings.Instance.DTRowKey;
                _proxy.SetKey(Encoding.ASCII.GetBytes(DTKey), Algorithm.DoubleTranposition);

                // Set XTEA Key and IV
                _proxy.SetKey(Encoding.ASCII.GetBytes(Settings.Instance.XTEAKey), Algorithm.XTEA);
                _proxy.SetIV(Encoding.ASCII.GetBytes(Settings.Instance.XTEAIV), Algorithm.XTEA);

                // Set Knapsack Key
                byte[] byteArray = Settings.Instance.KSPrivateKey.SelectMany(o => BitConverter.GetBytes(o)).ToArray();
                _proxy.SetKey(byteArray, Algorithm.Knapsack);

                // Generate Property Dictionary
                var Params = new Dictionary<string, byte[]>();
                Params.Add("rounds", BitConverter.GetBytes(Settings.Instance.XTEARounds));
                Params.Add("ofbModeXTEA", BitConverter.GetBytes(Settings.Instance.XTEAOutputFeedback));
                Params.Add("n", BitConverter.GetBytes(Settings.Instance.KSn));
                Params.Add("m", BitConverter.GetBytes(Settings.Instance.KSm));
                Params.Add("invm", BitConverter.GetBytes(Settings.Instance.KSmInverse));

                // Set properties
                _proxy.SetProperties(Params, Algorithm.XTEA);
                _proxy.SetProperties(Params, Algorithm.Knapsack);                
            }
            catch
            {
                // Attempt to reconnect if the service cannot be accessed
                if (DialogResult.No == MessageBox.Show("Cannot connect to service. Try connecting again?",
                        "Reconnect", MessageBoxButtons.YesNo))
                    Close();
                Thread.Sleep(1000);
                SetKeys();
            }
        }

        // Gets all files that have been created in the watched folder after a certain date
        private void GetFiles()
        {
            if (!Settings.Instance.FswEnabled) return;
            var directory = new DirectoryInfo(Settings.Instance.FswInput);
            var current = DateTime.Now;
            var files = directory.GetFiles()
                .Where(file => file.CreationTime >= Settings.Instance.ProcessClosedDate && file.CreationTime <= current);
            foreach (var file in files)
                _processor.EnqueueFileName(file.FullName);
        }

        // Event handler for created files
        private void watcher_Created(object sender, FileSystemEventArgs e)
        {
             _processor.EnqueueFileName(e.FullPath);
        }

        #endregion

        #region Form Methods
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Settings.Instance.FswEnabled) Settings.Instance.ProcessClosedDate = DateTime.Now;
            Settings.Instance.Save("settings.xml");
            _watcher?.Dispose();
            _processor?.Dispose();
        }
        

        private void fSWOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fSWOnOffToolStripMenuItem.Checked = !fSWOnOffToolStripMenuItem.Checked;
            Settings.Instance.FswEnabled = !Settings.Instance.FswEnabled;
            _watcher.EnableRaisingEvents = Settings.Instance.FswEnabled;
            if(Settings.Instance.FswEnabled) GetFiles();
            else Settings.Instance.ProcessClosedDate = DateTime.Now;
        }
        
        private void doubleTranspositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetAlgo(Algorithm.DoubleTranposition);
        }

        private void xTEAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetAlgo(Algorithm.XTEA);
        }

        private void knapsackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetAlgo(Algorithm.Knapsack);
        }

        private void mD5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetAlgo(Algorithm.MD5);
        }

        private void fSWInputFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fbdInput.ShowDialog() != DialogResult.OK) return;
            Settings.Instance.FswInput = fbdInput.SelectedPath;
            _watcher.Path = Settings.Instance.FswInput;
            fSWInputFolderToolStripMenuItem.ToolTipText = fbdInput.SelectedPath;
        }

        private void fSWOutputFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fbdOutput.ShowDialog() != DialogResult.OK) return;
            Settings.Instance.FswOutput = fbdOutput.SelectedPath;
            fSWOutputFolderToolStripMenuItem.ToolTipText = fbdOutput.SelectedPath;
        }

        private void swapBtn_Click(object sender, EventArgs e)
        {
            inputText.Text = outputText.Text;
            outputText.Clear();
        }

        private void encryptBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Do not allow encryption if input is empty unless MD5 is used
                if (IsEmpty(inputText) && Settings.Instance.Algo != Algorithm.MD5) return;

                // Encrypt data
                var inputBytes = Encoding.ASCII.GetBytes(inputText.Text);
                var outputBytes = _proxy.Crypt(inputBytes, Settings.Instance.Algo);
                
                // Display text as hex values
                outputText.Text = BitConverter.ToString(outputBytes);
                
            }
            catch (Exception exception)
            {
                outputText.Text = exception.InnerException.Message;
            }
        }

        private void decryptBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Return if there is nothing to decrypt
                if (IsEmpty(inputText)) return;

                // Text is always displayed as hex values
                byte[] inputBytes = inputText.Text.Split('-').Select(b => Convert.ToByte(b, 16)).ToArray();
               

                // Decrypt data
                var outputBytes = _proxy.DeCrypt(inputBytes, Settings.Instance.Algo);
                outputText.Text = Encoding.ASCII.GetString(outputBytes);
            }
            catch (Exception exception)
            {
                outputText.Text = exception.Message;
            }
        }

        private void encryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Settings.Instance.FswDecrypt) return;
            Settings.Instance.FswDecrypt = false;
            encryptToolStripMenuItem.Checked = true;
            decryptToolStripMenuItem.Checked = false;
        }

        private void decryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Settings.Instance.FswDecrypt) return;
            Settings.Instance.FswDecrypt = true;
            encryptToolStripMenuItem.Checked = false;
            decryptToolStripMenuItem.Checked = true;
        }

        private void parametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var keyForm = new KeyForm(_proxy);
            keyForm.ShowDialog();
        }

        private void cloudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cloudForm = new CloudForm();
            cloudForm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetKeys();
        }

        #endregion

    }
}
