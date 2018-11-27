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

        // Cloud Service Variables
        private const string CloudKey = "This is the cloud server key";
        private static byte[] CryptedCloudKey;
        private static XTEA CloudCypherXTEA;
        private const string StoragePath = "C://Remote//";
        private const int Limit = 6400000;

        // Delegate Dictionary for encryption/decryption methods
        private const int AlgoNumber = 5;
        private static Dictionary<int, Func<byte[], byte[]>> EncryptionDictionary;
        private static Dictionary<int, Func<byte[], bool>> KeyDictionary;
        private static Dictionary<int, Func<IDictionary<string, byte[]>, bool>> PropertyDictionary;

        #endregion

        #region Constructors

        public CryptoService()
        {
            Initialize();
        }

        #endregion

        #region Methods

        private static void Initialize()
        {
            // Initializing objects used for encryption
            if (CypherDoubleTransposition == null)
                CypherDoubleTransposition = new DoubleTransposition();

            if (CypherXTEA == null)
                CypherXTEA = new XTEA();

            if (CloudCypherXTEA == null)
                CloudCypherXTEA = new XTEA();

            if (CypherOFB == null)
                CypherOFB = new OFB();

            if (CypherKnapsack == null)
                CypherKnapsack = new Knapsack();

            if (CypherMD5 == null)
                CypherMD5 = new MD5();

            // Creating cloud storage directory
            if (!Directory.Exists(StoragePath))
            {
                var dir = Directory.CreateDirectory(StoragePath);
                dir.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            // Initializing cloud key
            if (CryptedCloudKey == null)
            {
                CryptedCloudKey = CypherMD5.Crypt(Encoding.ASCII.GetBytes(CloudKey));
                CloudCypherXTEA.SetKey(CryptedCloudKey);
            }

            // Initializing algorithm dictionary
            if(EncryptionDictionary == null)
                EncryptionDictionary
                    = new Dictionary<int, Func<byte[], byte[]>>()
                    {
                        // Encryption methods
                        { (int)Algorithm.DoubleTranposition, CypherDoubleTransposition.Crypt },
                        { (int)Algorithm.XTEA, CypherXTEA.Crypt },
                        { (int)Algorithm.OFB, CypherOFB.Crypt },
                        { (int)Algorithm.Knapsack, CypherKnapsack.Crypt },
                        { (int)Algorithm.MD5, CypherMD5.Crypt },

                        // Decryption methods, enum value + number of algorithms
                        { (int)Algorithm.DoubleTranposition + AlgoNumber, CypherDoubleTransposition.Decrypt },
                        { (int)Algorithm.XTEA + AlgoNumber, CypherXTEA.Decrypt },
                        { (int)Algorithm.OFB + AlgoNumber, CypherOFB.Decrypt },
                        { (int)Algorithm.Knapsack + AlgoNumber, CypherKnapsack.Decrypt },
                        { (int)Algorithm.MD5 + AlgoNumber, CypherKnapsack.Decrypt }
                    };
            
            // Initializing key dictionary
            if(KeyDictionary == null)
                KeyDictionary
                    = new Dictionary<int, Func<byte[], bool>>()
                    {
                        // Set Key methods
                        { (int)Algorithm.DoubleTranposition, CypherDoubleTransposition.SetKey },
                        { (int)Algorithm.XTEA, CypherXTEA.SetKey },
                        { (int)Algorithm.OFB, CypherOFB.SetKey },
                        { (int)Algorithm.Knapsack, CypherKnapsack.SetKey },
                        { (int)Algorithm.MD5, CypherMD5.SetKey }
                    };

            // Initializing property dictionary
            if(PropertyDictionary == null)
                PropertyDictionary
                    = new Dictionary<int, Func<IDictionary<string, byte[]>, bool>>()
                    {
                        // Set Property methods
                        { (int)Algorithm.DoubleTranposition, CypherDoubleTransposition.SetAlgorithmProperties },
                        { (int)Algorithm.XTEA, CypherXTEA.SetAlgorithmProperties },
                        { (int)Algorithm.OFB, CypherOFB.SetAlgorithmProperties },
                        { (int)Algorithm.Knapsack, CypherKnapsack.SetAlgorithmProperties },
                        { (int)Algorithm.MD5, CypherMD5.SetAlgorithmProperties },
                    };
        }

        private static void LimitCheck(byte[] arr)
        {
            try
            {
                if (arr.Length > Limit)
                    throw new ArgumentException("File size is over the maximum limit");
            }
            catch (ArgumentException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        #endregion

        #region Interface Methods

        #region Crypto Service

        public byte[] Crypt(byte[] input, Algorithm a)
        {
            try
            {
                LimitCheck(input);
                return EncryptionDictionary[(int)a](input);
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
                return EncryptionDictionary[(int) a + AlgoNumber](input);
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
                LimitCheck(input);
                return KeyDictionary[(int)a](input);
            }
            catch (Exception exception)
            {
                throw new FaultException(exception.Message);
            }
        }

        public bool SetProperties(IDictionary<string, byte[]> specArguments, Algorithm a)
        {
            try
            {
                return PropertyDictionary[(int) a](specArguments);
            }
            catch (Exception exception)
            {
                throw new FaultException(exception.Message);
            }
        }

        public bool SetIV(byte[] input)
        {
            LimitCheck(input);
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
