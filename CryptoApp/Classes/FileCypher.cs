using System;
using System.IO;
using System.Text;
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
                var buff = File.ReadAllBytes(filename);
                var outputBytes = _proxy.Crypt(buff, Settings.Instance.Algo);
                var newFileName = Path.GetFileNameWithoutExtension(filename) + "_" + Guid.NewGuid() +
                                  Path.GetExtension(filename);
                File.WriteAllBytes(Settings.Instance.FswOutput + "//" + newFileName, outputBytes);
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error encrypting file " + filename + "\r\n" + exception.Message);
                return false;
            }
        }

        public bool DecryptFile(string filename)
        {
            try
            {
                var buff = File.ReadAllBytes(filename);
                var outputBytes = _proxy.DeCrypt(buff, Settings.Instance.Algo);
                var name = Path.GetFileNameWithoutExtension(filename).Split('_')[0];
                var newFileName = name + Path.GetExtension(filename);
                File.WriteAllText(Settings.Instance.FswOutput + "//" + newFileName,
                    Encoding.ASCII.GetString(outputBytes));
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error decrypting file " + filename + "\r\n" + exception.Message);
                return false;
            }
        }

        #endregion

    }
}
