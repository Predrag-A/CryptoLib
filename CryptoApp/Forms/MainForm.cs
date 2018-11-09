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

namespace CryptoApp
{
    public partial class MainForm : Form
    {


        #region Constructors

        public MainForm()
        {
            InitializeComponent();

            Settings.Instance.Load("settings.xml");
        }

        #endregion

        #region Methods

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Instance.Save("settings.xml");
        }

        #endregion

    }
}
