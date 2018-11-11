using System.Collections.Generic;

namespace CryptoLib
{
    internal interface ICrypto
    {

        #region Interface Methods

        bool SetKey(byte[] input);

        byte[] GenerateRandomKey();

        bool SetIV(byte[] input);

        byte[] GenerateRandomIV();

        bool SetAlgorithmProperties(IDictionary<string, byte[]> specArguments);

        byte[] Crypt(byte[] input);

        byte[] Decrypt(byte[] output);

        #endregion

    }
}
