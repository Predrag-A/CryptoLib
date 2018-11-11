using System;
using System.Collections.Generic;

namespace CryptoLib
{
    public class OFB:ICrypto
    {

        #region Interface Methods

        public bool SetKey(byte[] input)
        {
            throw new NotImplementedException();
        }

        public byte[] GenerateRandomKey()
        {
            throw new NotImplementedException();
        }

        public bool SetIV(byte[] input)
        {
            throw new NotImplementedException();
        }

        public byte[] GenerateRandomIV()
        {
            throw new NotImplementedException();
        }

        public bool SetAlgorithmProperties(IDictionary<string, byte[]> specArguments)
        {
            throw new NotImplementedException();
        }

        public byte[] Crypt(byte[] input)
        {
            throw new NotImplementedException();
        }

        public byte[] Decrypt(byte[] output)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
