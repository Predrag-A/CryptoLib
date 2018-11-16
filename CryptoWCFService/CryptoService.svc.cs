using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Text;
using CryptoLib;

namespace CryptoWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CryptoService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CryptoService.svc or CryptoService.svc.cs at the Solution Explorer and start debugging.
    public class CryptoService : ICryptoService
    {

        #region Fields

        // Crypto Service Variables
        private static DoubleTransposition CypherDoubleTransposition;
        private static XTEA CypherXTEA;
        private static OFB CypherOFB;
        private static Knapsack CypherKnapsack;
        private static MD5 CypherMD5;
        private static string StoragePath;

        // Cloud Service Variables
        private const string CloudKey = "This is the cloud server key";
        private readonly byte[] CryptedCloudKey;
        private static XTEA CloudCypherXTEA;

        #endregion

        #region Constructors

        public CryptoService()
        {
            if(CypherDoubleTransposition == null)
                CypherDoubleTransposition = new DoubleTransposition();

            if(CypherXTEA == null)
                CypherXTEA = new XTEA();

            if (CloudCypherXTEA == null)
                CloudCypherXTEA = new XTEA();

            if(CypherOFB == null)
                CypherOFB = new OFB();

            if(CypherKnapsack == null)
                CypherKnapsack = new Knapsack();

            if(CypherMD5 == null)
                CypherMD5 = new MD5();

            if (StoragePath == null)
            {
                StoragePath = "C://Remote//";
                Directory.CreateDirectory(StoragePath);
            }

            if (CryptedCloudKey != null) return;
            CryptedCloudKey = CypherMD5.Crypt(Encoding.ASCII.GetBytes(CloudKey));
            CloudCypherXTEA.SetKey(CryptedCloudKey);
        }

        #endregion

        #region Interface Methods

        #region Crypto Service

        public byte[] Crypt(byte[] input, Algorithm a)
        {
            try
            {
                switch (a)
                {
                    case Algorithm.DoubleTranposition:
                        return CypherDoubleTransposition.Crypt(input);
                    case Algorithm.XTEA:
                        return CypherXTEA.Crypt(input);
                    case Algorithm.OFB:
                        return CypherOFB.Crypt(input);
                    case Algorithm.Knapsack:
                        return CypherKnapsack.Crypt(input);
                    default:
                        return CypherMD5.Crypt(input);
                }
            }
            catch (Exception exception)
            {
                throw new FaultException(exception.Message);
            }
        }

        public byte[] DeCrypt(byte[] input, Algorithm a)
        {
            try
            {
                switch (a)
                {
                    case Algorithm.DoubleTranposition:
                        return CypherDoubleTransposition.Decrypt(input);
                    case Algorithm.XTEA:
                        return CypherXTEA.Decrypt(input);
                    case Algorithm.OFB:
                        return CypherOFB.Decrypt(input);
                    case Algorithm.Knapsack:
                        return CypherKnapsack.Decrypt(input);
                    default:
                        return CypherMD5.Decrypt(input);
                }
            }
            catch (Exception exception)
            {
                throw new FaultException(exception.Message);
            }
        }

        public bool SetKey(byte[] input, Algorithm a)
        {
            try
            {
                switch (a)
                {
                    case Algorithm.DoubleTranposition:
                        return CypherDoubleTransposition.SetKey(input);
                    case Algorithm.XTEA:
                        return CypherXTEA.SetKey(input);
                    case Algorithm.OFB:
                        return CypherOFB.SetKey(input);
                    case Algorithm.Knapsack:
                        return CypherKnapsack.SetKey(input);
                    default:
                        return true;
                }
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool SetProperties(IDictionary<string, byte[]> specArguments)
        {
            throw new NotImplementedException();
        }

        public bool SetIV(byte[] input)
        {
            throw new NotImplementedException();
        }

        public byte[][] RandomizeKeys()
        {
            var keys = new byte[2][];
            keys[0] = CypherDoubleTransposition.GenerateRandomKey();
            keys[1] = CypherXTEA.GenerateRandomKey();

            return keys;
        }

        public byte[] RandomizeIV()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Cloud Service

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

        public byte[] DownloadFile(string fileName)
        {
            try
            {
                var fullPath = StoragePath + "//" + fileName;
                if (!File.Exists(fullPath)) throw new ArgumentException("File does not exist");
                
                var buff = File.ReadAllBytes(fullPath);
                var outputBytes = CloudCypherXTEA.Decrypt(buff);

                return outputBytes;
            }
            catch (Exception exception)
            {
                throw new FaultException(exception.Message);
            }
        }

        public int UploadFile(string fileName)
        {
            try
            {
                // Save encrypted file in remote directory
                var buff = File.ReadAllBytes(fileName);
                var outputBytes = CloudCypherXTEA.Crypt(buff);
                
                var name = Path.GetFileNameWithoutExtension(fileName);
                var extension = Path.GetExtension(fileName);
                var fullPath = StoragePath + "//" + Path.GetFileName(fileName);
                var newFullPath = fullPath;
                var count = 0;

                // Check if file exists as to not overwrite file
                while (File.Exists(newFullPath))
                {
                    count++;
                    newFullPath = StoragePath + "//" + name + "(" + count + ")" + extension;
                }

                File.WriteAllBytes(newFullPath, outputBytes);
                return count;
            }
            catch (Exception exception)
            {
                throw new FaultException(exception.Message);
            }
        }

        #endregion

        #endregion

    }
}
