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
                using (var inputStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None))
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
                var newFileName = Guid.NewGuid() + Path.GetFileNameWithoutExtension(filename) +
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
                var name = Path.GetFileName(filename);

                // Remove GUID from name
                var newFileName = name.Substring(36);

                File.WriteAllBytes(Settings.Instance.FswOutput + "//" + newFileName,
                    outputBytes);
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
