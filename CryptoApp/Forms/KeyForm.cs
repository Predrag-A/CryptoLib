using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            txtKeyColumnDT.Text = Settings.Instance.DTColKey;
            txtKeyRowDT.Text = Settings.Instance.DTRowKey;
            txtKeyXTEA.Text = Settings.Instance.XTEAKey;
            numRoundsXTEA.Value = Settings.Instance.XTEARounds;
            txtKeyPrivateKS.Text = Settings.Instance.KSPrivateKey;
            txtKeyPublicKS.Text = Settings.Instance.KSPublicKey;
        }

        #endregion

        #region Form Methods

        private void btnKeyDT_Click(object sender, EventArgs e)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(txtKeyColumnDT.Text + "," + txtKeyRowDT.Text);
                if(_proxy.SetKey(key, Algorithm.DoubleTranposition))
                {
                    statusLbl.Text = "Status: Double Transposition keys successfully set";
                    Settings.Instance.DTColKey = txtKeyColumnDT.Text;
                    Settings.Instance.DTRowKey = txtKeyRowDT.Text;
                }
                else
                {
                    statusLbl.Text = "Status: Unable to set Double Transposition keys";
                }
            }
            catch (Exception exception)
            {
                statusLbl.Text = "Status: " + exception.Message;
            }
        }

        private void btnKeyXTEA_Click(object sender, EventArgs e)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(txtKeyXTEA.Text);
                if (_proxy.SetKey(key, Algorithm.XTEA))
                {
                    statusLbl.Text = "Status: XTEA key successfully set";
                    Settings.Instance.XTEAKey = txtKeyXTEA.Text;
                }
                else
                {
                    statusLbl.Text = "Status: Unable to set XTEA key";
                }
            }
            catch (Exception exception)
            {
                statusLbl.Text = "Status: " + exception.Message;
            }
        }

        private void btnKeyKS_Click(object sender, EventArgs e)
        {

        }

        private void btnParamXTEA_Click(object sender, EventArgs e)
        {

        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes ==
                MessageBox.Show("Are you sure you want to randomize keys?", "Warning", MessageBoxButtons.YesNo))
                statusLbl.Text = "Keys randomized";
        }

        private void txtDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        #endregion

    }
}
