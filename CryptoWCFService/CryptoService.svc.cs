using System;
using System.Collections.Generic;
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

        private static DoubleTransposition CypherDoubleTransposition;
        private static XTEA CypherXTEA;
        private static OFB CypherOFB;
        private static Knapsack CypherKnapsack;
        private static MD5 CypherMD5;

        #endregion

        #region Constructors

        public CryptoService()
        {
            if(CypherDoubleTransposition == null)
                CypherDoubleTransposition = new DoubleTransposition();

            if(CypherXTEA == null)
                CypherXTEA = new XTEA();

            if(CypherOFB == null)
                CypherOFB = new OFB();

            if(CypherKnapsack == null)
                CypherKnapsack = new Knapsack();

            if(CypherMD5 == null)
                CypherMD5 = new MD5();
        }

        #endregion

        #region Interface Methods

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

    }
}
