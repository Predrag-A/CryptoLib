using System;
using System.IO;
using System.ServiceModel;
using System.Text;
using CryptoLib;

namespace CryptoWCFService
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class CloudService : ICloudService
    {

        #region Fields


        // Cloud Service Variables
        private const string CloudKey = "This is the cloud server key";
        private static byte[] CryptedCloudKey;
        private static XTEA CloudCypherXTEA;
        private const string StoragePath = "C://Remote//";
        private const int ChunkSize = 2048;


        #endregion

        #region Constructors

        public CloudService()
        {
            Initialize();
        }

        #endregion

        #region Methods

        private static void Initialize()
        {
            // Initializing XTEA encryption object
            if (CloudCypherXTEA == null)
                CloudCypherXTEA = new XTEA();

            // Creating cloud storage directory
            if (!Directory.Exists(StoragePath))
            {
                var dir = Directory.CreateDirectory(StoragePath);
                dir.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            // Initializing cloud key
            if (CryptedCloudKey != null) return;
            MD5 CypherMD5 = new MD5();
            CryptedCloudKey = CypherMD5.Crypt(Encoding.ASCII.GetBytes(CloudKey));
            CloudCypherXTEA.SetKey(CryptedCloudKey);
        }

        #endregion

        #region Interface Methods

        public RemoteFileInfo DownloadFile(DownloadRequest request)
        {
            try
            {
                // Get info about the input file
                var filePath = Path.Combine(StoragePath, request.FileName);
                var fileInfo = new FileInfo(filePath);

                // Check if file exists
                if (!fileInfo.Exists) throw new FileNotFoundException("File not found", request.FileName);
                
                // Open stream
                // After returning to the client the download starts.
                // Stream remains open and on the server and the client reads it.
                // Files will be decrypted client-side
                var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                // Return stream
                return new RemoteFileInfo
                {
                    FileName = request.FileName,
                    Length = fileInfo.Length,
                    FileByteStream = stream
                };
            }
            catch (Exception exception)
            {
                throw new FaultException(exception.Message);
            }
        }

        public UploadReply UploadFile(RemoteFileInfo request)
        {
            try
            {
                // Create file name
                var name = Path.GetFileNameWithoutExtension(request.FileName);
                var extension = Path.GetExtension(request.FileName);
                var fullPath = StoragePath + "//" + Path.GetFileName(request.FileName);
                var newFullPath = fullPath;
                var count = 0;

                // Check if file exists as to not overwrite the file
                while (File.Exists(newFullPath))
                {
                    count++;
                    newFullPath = StoragePath + "//" + name + "(" + count + ")" + extension;
                }

                using (var writeStream = new FileStream(newFullPath, FileMode.CreateNew, FileAccess.Write))
                {
                    do
                    {

                        // Initializing buffer
                        var buffer = new byte[2048];

                        // Read bytes from the input stream
                        int bytesRead = request.FileByteStream.Read(buffer, 0, ChunkSize);
                        if (bytesRead == 0) break;

                        // If the last chunk is being processed
                        if (bytesRead < ChunkSize)
                        {
                            var temp = new byte[bytesRead];
                            Array.Copy(buffer, temp, bytesRead);
                            buffer = temp;
                        }

                        // Temporary buffer containing encrypted data
                        var cryptedBuffer = CloudCypherXTEA.Crypt(buffer);

                        // Write bytes to the output stream
                        writeStream.Write(cryptedBuffer, 0, cryptedBuffer.Length);

                    } while (true);

                    writeStream.Close();
                }

                // Return result
                return new UploadReply {FileName = Path.GetFileName(newFullPath)};

            }
            catch (Exception exception)
            {
                throw new FaultException(exception.Message);
            }
        }

        public string[] GetFileList()
        {
            try
            {
                // Return all file names in remote directory
                var fileNames = Directory.GetFiles(StoragePath);
                return fileNames;
            }
            catch (Exception exception)
            {
                throw new FaultException(exception.Message);
            }
        }

        public string GetKey()
        {
            try
            {
                return CloudKey;
            }
            catch (Exception exception)
            {
                throw new FaultException(exception.Message);
            }
        }

        public bool DeleteFile(string fileName)
        {
            try
            {
                var fullPath = StoragePath + "//" + fileName;
                if (!File.Exists(fullPath)) return false;

                File.Delete(fullPath);
                return true;
            }
            catch (Exception exception)
            {
                throw new FaultException(exception.Message);
            }
        }

        #endregion

    }
}
