using System;
using System.IO;
using System.Windows.Forms;
using CryptoApp.CryptoServiceReference;

namespace CryptoApp.Classes
{
    public class FileCypher
    {

        #region Fields

        private ICryptoService _proxy = new CryptoServiceClient();

        #endregion

        #region Methods

        public static bool IsFileReady(string filename)
        {
            try
            {
                using (FileStream inputStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None))
                    return inputStream.Length > 0;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public bool CryptFile(string filename)
        {
            try
            {
                byte[] buff = File.ReadAllBytes(filename);
                var outputBytes = _proxy.Crypt(buff, Settings.Instance.Algo);
                File.WriteAllText(Settings.Instance.FswOutput + "//Test.txt", BitConverter.ToString(outputBytes).Replace("-", "").ToLower());
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return false;
            }
        }

        #endregion

    }
}
