using System;
using System.Collections.Generic;
using System.ServiceModel;
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
        private static Knapsack CypherKnapsack;
        private static MD5 CypherMD5;
        private const int Limit = 6400000;

        // Delegate Dictionary for encryption/decryption methods
        private const int AlgoNumber = 4;
        private static Dictionary<int, Func<byte[], byte[]>> EncryptionDictionary;
        private static Dictionary<int, Func<byte[], bool>> KeyIVDictionary;
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

            if (CypherKnapsack == null)
                CypherKnapsack = new Knapsack();

            if (CypherMD5 == null)
                CypherMD5 = new MD5();
            
            // Initializing algorithm dictionary
            if(EncryptionDictionary == null)
                EncryptionDictionary
                    = new Dictionary<int, Func<byte[], byte[]>>()
                    {
                        // Encryption methods
                        { (int)Algorithm.DoubleTranposition, CypherDoubleTransposition.Crypt },
                        { (int)Algorithm.XTEA, CypherXTEA.Crypt },
                        { (int)Algorithm.Knapsack, CypherKnapsack.Crypt },
                        { (int)Algorithm.MD5, CypherMD5.Crypt },

                        // Decryption methods, enum value + number of algorithms
                        { (int)Algorithm.DoubleTranposition + AlgoNumber, CypherDoubleTransposition.Decrypt },
                        { (int)Algorithm.XTEA + AlgoNumber, CypherXTEA.Decrypt },
                        { (int)Algorithm.Knapsack + AlgoNumber, CypherKnapsack.Decrypt },
                        { (int)Algorithm.MD5 + AlgoNumber, CypherKnapsack.Decrypt }
                    };
            
            // Initializing key and IV dictionary
            if(KeyIVDictionary == null)
                KeyIVDictionary
                    = new Dictionary<int, Func<byte[], bool>>()
                    {
                        // Set Key methods
                        { (int)Algorithm.DoubleTranposition, CypherDoubleTransposition.SetKey },
                        { (int)Algorithm.XTEA, CypherXTEA.SetKey },
                        { (int)Algorithm.Knapsack, CypherKnapsack.SetKey },
                        { (int)Algorithm.MD5, CypherMD5.SetKey },

                        // Set IV methods, enum value + number of algorithms
                        { (int)Algorithm.DoubleTranposition + AlgoNumber, CypherDoubleTransposition.SetIV },
                        { (int)Algorithm.XTEA + AlgoNumber, CypherXTEA.SetIV },
                        { (int)Algorithm.Knapsack + AlgoNumber, CypherKnapsack.SetIV },
                        { (int)Algorithm.MD5 + AlgoNumber, CypherMD5.SetIV }
                    };

            // Initializing property dictionary
            if(PropertyDictionary == null)
                PropertyDictionary
                    = new Dictionary<int, Func<IDictionary<string, byte[]>, bool>>()
                    {
                        // Set Property methods
                        { (int)Algorithm.DoubleTranposition, CypherDoubleTransposition.SetAlgorithmProperties },
                        { (int)Algorithm.XTEA, CypherXTEA.SetAlgorithmProperties },
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
                return KeyIVDictionary[(int)a](input);
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

        public bool SetIV(byte[] input, Algorithm a)
        {
            try
            {
                LimitCheck(input);
                return KeyIVDictionary[(int) a + AlgoNumber](input);
            }
            catch (Exception exception)
            {
                throw new FaultException(exception.Message);
            }

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

    }
}
