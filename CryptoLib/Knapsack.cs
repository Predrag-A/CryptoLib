using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CryptoLib
{
    public class Knapsack: ICrypto
    {

        #region Fields

        // Length of data being crypted in bits
        private const byte DataLength = 8;

        // Keys
        private uint[] _privateKey;
        private uint[] _publicKey;

        // Knapsack variables
        private uint _n;
        private uint _m;
        private uint _mInverse;

        #endregion

        #region Constructors

        public Knapsack()
        {
            // Default values
            _n = 491;
            _m = 41;
            _mInverse = 12;

            _privateKey = new uint[] { 2, 3, 7, 14, 30, 57, 120, 251 };
            _publicKey = new uint[] { 82, 123, 287, 83, 248, 373, 10, 471 };

        }

        #endregion

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

            // Getting input length
            var length = input.Length;

            // Initializing result array
            var result = new uint[length];

            // Main loop
            for (var i = 0; i < length; i++)
            {
                // Get value of current byte as byte array
                var inputByte = new [] { input[i] };

                // Get bit array of current byte
                var bits = new BitArray(inputByte);

                // Calculating crypted value for current byte
                for (var j = 0; j < DataLength; j++)
                    if (bits[DataLength- 1 - j])
                        result[i] += _publicKey[j];
            }

            // Converting uint array to string
            var sb = new StringBuilder();
            for (var i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString());
                if (i != result.Length - 1)
                    sb.Append(" ");
            }
            
            // Converting string to byte
            var byteResult = Encoding.ASCII.GetBytes(sb.ToString());

            return byteResult;

        }

        public byte[] Decrypt(byte[] output)
        {
            // Getting input as string array
            var stringOutput = Encoding.ASCII.GetString(output).Split(' ');

            // Getting uint array
            var array = Array.ConvertAll(stringOutput, uint.Parse);

            // Getting input length
            var length = array.Length;

            // Initializing result array
            var result = new byte[length];

            // Main loop
            for (var i = 0; i < length; i++)
            {
                // Calculating transformed crypted data
                var TC = (array[i] * _mInverse) % _n;

                // Temporary bit array
                var bits = new BitArray(DataLength);
                
                // Current value
                var current = TC;

                // Calculating decrypted byte value
                for (var j = DataLength - 1; j >= 0; j--)
                {
                    // Unsigned integers, if private key value is larger there is overflow
                    if (current - _privateKey[j] >= current) continue;
                    bits[DataLength - 1 - j] = true;
                    current -= _privateKey[j];
                }

                // Saving result
                bits.CopyTo(result, i);
            }

            return result;
        }

        #endregion

    }
}
