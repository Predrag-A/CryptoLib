using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CryptoApp.Classes;
using CryptoApp.CryptoServiceReference;
using CryptoLib;

namespace CryptoApp.Forms
{
    public partial class KeyForm : Form
    {

        #region Fields

        private readonly ICryptoService _proxy;

        // Array of knapsack key numerics
        private NumericUpDown[] _numerics;
        private readonly Dictionary<string, byte[]> _params = new Dictionary<string, byte[]>();

        #endregion

        #region Constructors

        public KeyForm(ICryptoService p)
        {
            _proxy = p;
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            // Initialize array of numerics to more easily access Knapsack key
            _numerics = new[] { numKS0, numKS1, numKS2, numKS3, numKS4, numKS5, numKS6, numKS7 };

            // Initialize Double Transposition values
            txtKeyColumnDT.Text = Settings.Instance.DTColKey;
            txtKeyRowDT.Text = Settings.Instance.DTRowKey;
            checkOFBDT.Checked = Settings.Instance.DTOutputFeedback;

            // Initialize XTEA values
            txtKeyXTEA.Text = Settings.Instance.XTEAKey;
            txtIVXTEA.Text = Settings.Instance.XTEAIV;
            numRoundsXTEA.Value = Settings.Instance.XTEARounds;
            checkOFBXTEA.Checked = Settings.Instance.XTEAOutputFeedback;

            // Initialize Knapsack values
            numKSN.Value = Settings.Instance.KSn;
            numKSM.Value = Settings.Instance.KSm;
            numKSInvM.Value = Settings.Instance.KSmInverse;
            for (var i = 0; i < _numerics.Length; i++)
                _numerics[i].Value = Settings.Instance.KSPrivateKey[i];

            // Initialize dictionary values
            _params.Add("ofbModeDT", BitConverter.GetBytes(Settings.Instance.DTOutputFeedback));
            _params.Add("rounds", BitConverter.GetBytes(Settings.Instance.XTEARounds));
            _params.Add("n", BitConverter.GetBytes(Settings.Instance.KSn));
            _params.Add("m", BitConverter.GetBytes(Settings.Instance.KSm));
            _params.Add("invm", BitConverter.GetBytes(Settings.Instance.KSmInverse));
            _params.Add("ofbModeXTEA", BitConverter.GetBytes(Settings.Instance.XTEAOutputFeedback));

        }

        private void Log(string str)
        {
            statusLbl.Text = "Status: " + str;
        }

        #endregion

        #region Form Methods

        #region Double Transposition

        // Set Double Transposition key
        private void btnKeyDT_Click(object sender, EventArgs e)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(txtKeyColumnDT.Text + "," + txtKeyRowDT.Text);
                if(_proxy.SetKey(key, Algorithm.DoubleTranposition))
                {
                   Log("Double Transposition keys successfully set");
                    Settings.Instance.DTColKey = txtKeyColumnDT.Text;
                    Settings.Instance.DTRowKey = txtKeyRowDT.Text;
                }
                else
                {
                    Log("Unable to set Double Transposition keys");
                }
            }
            catch (Exception exception)
            {
                Log(exception.Message);
            }
        }

        // Set Double Transposition properties
        private void btnParamDT_Click(object sender, EventArgs e)
        {
            try
            {
                _params["ofbModeDT"] = BitConverter.GetBytes(Convert.ToBoolean(checkOFBDT.Checked));
                if (_proxy.SetProperties(_params, Algorithm.DoubleTranposition))
                {
                    Settings.Instance.DTOutputFeedback = checkOFBDT.Checked;
                    Log("Double Transposition properties successfully set");
                }
                else
                {
                    Log("Double Transposition to set XTEA properties");
                }
            }
            catch (Exception exception)
            {
                Log(exception.Message);
            }
        }

        #endregion

        #region XTEA

        // Set XTEA key
        private void btnKeyXTEA_Click(object sender, EventArgs e)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(txtKeyXTEA.Text);
                if (_proxy.SetKey(key, Algorithm.XTEA))
                {
                    Log("XTEA key successfully set");
                    Settings.Instance.XTEAKey = txtKeyXTEA.Text;
                }
                else
                {
                    Log("Unable to set XTEA key");
                }
            }
            catch (Exception exception)
            {
                Log(exception.Message);
            }
        }

        // Set XTEA properties
        private void btnParamXTEA_Click(object sender, EventArgs e)
        {
            try
            {
                _params["ofbModeXTEA"] = BitConverter.GetBytes(Convert.ToBoolean(checkOFBXTEA.Checked));
                _params["rounds"] = BitConverter.GetBytes(Convert.ToUInt32(numRoundsXTEA.Value));
                if (_proxy.SetProperties(_params, Algorithm.XTEA))
                {
                    Settings.Instance.XTEARounds = (uint)numRoundsXTEA.Value;
                    Settings.Instance.XTEAOutputFeedback = checkOFBXTEA.Checked;
                    Log("XTEA properties successfully set");
                }
                else
                {
                    Log("Unable to set XTEA properties");
                }
            }
            catch (Exception exception)
            {
                Log(exception.Message);
            }
        }

        // Set XTEA initialization vector
        private void btnIVXTEA_Click(object sender, EventArgs e)
        {
            try
            {
                var iv = Encoding.ASCII.GetBytes(txtIVXTEA.Text);
                if (_proxy.SetIV(iv, Algorithm.XTEA))
                {
                    Log("XTEA initialization vector successfully set");
                    Settings.Instance.XTEAIV = txtIVXTEA.Text;
                }
                else
                {
                    Log("Unable to set XTEA initialization vector");
                }
            }
            catch (Exception exception)
            {
                Log(exception.Message);
            }
        }

        #endregion

        #region Knapsack

        // Set Knapsack key
        private void btnKeyKS_Click(object sender, EventArgs e)
        {
            try
            {

                // Get array of values from numeric inputs
                uint[] values = _numerics.Select(s => (uint) s.Value).ToArray();

                // Get byte array from values
                byte[] byteArray = values.SelectMany(BitConverter.GetBytes).ToArray();

                if (_proxy.SetKey(byteArray, Algorithm.Knapsack))
                {
                    Settings.Instance.KSPrivateKey = values;
                    Log("Knapsack key successfully set");
                }
                else
                {
                    Log("Unable to set Knapsack key");
                }

            }
            catch (Exception exception)
            {
                Log(exception.Message);
            }
        }

        // Set Knapsack properties
        private void btnParamKS_Click(object sender, EventArgs e)
        {
            try
            {
                _params["n"] = BitConverter.GetBytes(Convert.ToUInt32(numKSN.Value));
                _params["m"] = BitConverter.GetBytes(Convert.ToUInt32(numKSM.Value));
                _params["invm"] = BitConverter.GetBytes(Convert.ToUInt32(numKSInvM.Value));
                if (_proxy.SetProperties(_params, Algorithm.Knapsack))
                {
                    Settings.Instance.KSn = (uint)numKSN.Value;
                    Settings.Instance.KSm = (uint)numKSM.Value;
                    Settings.Instance.KSmInverse = (uint)numKSInvM.Value;
                    Log("Knapsack properties successfully set");
                }
                else
                {
                    Log("Unable to set Knapsack properties");
                }
            }
            catch (Exception exception)
            {
                Log(exception.Message);
            }
        }

        #endregion

        #region Other

        // Allow only letters, digits and control characters
        private void txtDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        #endregion

        #endregion

        
    }
}
