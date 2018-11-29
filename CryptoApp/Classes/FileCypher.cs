using System;
using System.IO;
using System.Windows.Forms;
using CryptoApp.CryptoServiceReference;

namespace CryptoApp.Classes
{
    public class FileCypher
    {

        #region Fields

        private readonly ICryptoService _proxy = new CryptoServiceClient();

        #endregion

        #region Methods
        
        public static bool IsFileReady(string filename)
        {
            // Return true if the file can be opened, false if it can't
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
                // Read bytes from file
                var buff = File.ReadAllBytes(filename);

                // Encrypt bytes via proxy
                var outputBytes = _proxy.Crypt(buff, Settings.Instance.Algo);

                // Generate new file name: GUID + oldFileName
                var newFileName = Guid.NewGuid() + Path.GetFileNameWithoutExtension(filename) +
                                  Path.GetExtension(filename);

                // Store bytes at output location specified in settings
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
                // Read bytes from file
                var buff = File.ReadAllBytes(filename);

                // Decrypt bytes via proxy
                var outputBytes = _proxy.DeCrypt(buff, Settings.Instance.Algo);
                
                // Remove GUID from name
                var newFileName = Path.GetFileName(filename).Substring(36);

                // Store bytes at output location specified in settings
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
