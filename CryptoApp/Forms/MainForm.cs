using System;
using System.Windows.Forms;
using CryptoApp.Classes;
using CryptoApp.CryptoServiceReference;
using CryptoLib;
using Encoding = System.Text.Encoding;

namespace CryptoApp
{
    public partial class MainForm : Form
    {

        #region Fields

        private ICryptoService proxy = new CryptoServiceClient();

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
            
        }

        private void CheckAlgo(Algorithm a)
        {
            switch (a)
            {
                case Algorithm.DoubleTranposition:
                    decryptBtn.Enabled = true;
                    doubleTranspositionToolStripMenuItem.Checked = true;
                    break;

                case Algorithm.OFB:
                    decryptBtn.Enabled = true;
                    oFBToolStripMenuItem.Checked = true;
                    break;

                case Algorithm.XTEA:
                    decryptBtn.Enabled = true;
                    xTEAToolStripMenuItem.Checked = true;
                    break;

                case Algorithm.MD5:
                    decryptBtn.Enabled = false;
                    mD5ToolStripMenuItem.Checked = true;
                    break;

                case Algorithm.Knapsack:
                    decryptBtn.Enabled = true;
                    knapsackToolStripMenuItem.Checked = true;
                    break;
            }
        }

        private void UncheckAlgo()
        {
            doubleTranspositionToolStripMenuItem.Checked = false;
            oFBToolStripMenuItem.Checked = false;
            xTEAToolStripMenuItem.Checked = false;
            mD5ToolStripMenuItem.Checked = false;
            knapsackToolStripMenuItem.Checked = false;
        }

        private void SetAlgo(Algorithm a)
        {
            if (Settings.Instance.Algo == a)
                return;
            Settings.Instance.Algo = a;
            UncheckAlgo();
            CheckAlgo(a);
        }

        #endregion

        #region Form Methods

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Instance.Save("settings.xml");
        }
        

        private void fSWOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fSWOnOffToolStripMenuItem.Checked = !fSWOnOffToolStripMenuItem.Checked;
            Settings.Instance.FswEnabled = !Settings.Instance.FswEnabled;
        }
        
        private void doubleTranspositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetAlgo(Algorithm.DoubleTranposition);
        }

        private void xTEAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetAlgo(Algorithm.XTEA);
        }

        private void oFBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetAlgo(Algorithm.OFB);
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
            var inputBytes = Encoding.ASCII.GetBytes(inputText.Text);
            var outputBytes = proxy.Crypt(inputBytes, Settings.Instance.Algo);
            outputText.Text = BitConverter.ToString(outputBytes).Replace("-","").ToLower();
        }

        private void decryptBtn_Click(object sender, EventArgs e)
        {
            var inputBytes = Encoding.ASCII.GetBytes(inputText.Text);
            var outputBytes = proxy.DeCrypt(inputBytes, Settings.Instance.Algo);
            outputText.Text = BitConverter.ToString(outputBytes);
        }

        #endregion
    }
}
