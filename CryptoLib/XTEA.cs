using System;
using System.Collections.Generic;
using System.IO;

namespace CryptoLib
{
    public class XTEA: ICrypto
    {

        #region Fields

        // MD5 hash generator used to set the key
        private readonly MD5 _md5 = new MD5();

        // Number of Feistel-cipher rounds
        private uint _rounds;

        // Key used for encrypting/decrypting data
        private byte[] _key;

        // Contains delta value used for encrypting data
        private const uint DELTA = 0x9E377989;

        // Contains recommended rounds value
        private const uint ROUNDS = 32;

        #endregion

        #region Constructors

        public XTEA()
        {
            _rounds = ROUNDS;
            _key = GenerateRandomKey();
        }

        #endregion

        #region Methods

        // Encrypts a block of 64-bit data contained in 2 32-bit values
        private void Crypt(uint[] v, uint[] key)
        {
            uint v0 = v[0], v1 = v[1], sum = 0;
            for (var i = 0; i < _rounds; i++)
            {
                v0 += (((v1 << 4) ^ (v1 >> 5)) + v1) ^ (sum + key[sum & 3]);
                sum += DELTA;
                v1 += (((v0 << 4) ^ (v0 >> 5)) + v0) ^ (sum + key[(sum >> 11) & 3]);
            }

            v[0] = v0;
            v[1] = v1;
        }

        private void Decrypt(uint[] v, uint[] key)
        {
            uint v0 = v[0], v1 = v[1], sum = DELTA * _rounds;
            for (uint i = 0; i < _rounds; i++)
            {
                v1 -= (((v0 << 4) ^ (v0 >> 5)) + v0) ^ (sum + key[(sum >> 11) & 3]);
                sum -= DELTA;
                v0 -= (((v1 << 4) ^ (v1 >> 5)) + v1) ^ (sum + key[sum & 3]);
            }

            v[0] = v0;
            v[1] = v1;
        }

        #endregion

        #region Interface Methods

        // Key gets converted to MD5 hash
        public bool SetKey(byte[] input)
        {
            _key = input.Length != 16 ? _md5.Crypt(input) : input;
            return true;
        }

        // Generates random 128-bit key
        public byte[] GenerateRandomKey()
        {
            var rand = new Random();
            var b = new byte[16];
            rand.NextBytes(b);
            return b;
        }

        // XTEA does not use an initialization vector
        public bool SetIV(byte[] input)
        {
            throw new ArgumentException("XTEA does not use an initialization vector.");
        }

        public byte[] GenerateRandomIV()
        {
            throw new ArgumentException("XTEA does not use an initialization vector.");
        }

        // Can be used to set key and round numbers
        public bool SetAlgorithmProperties(IDictionary<string, byte[]> specArguments)
        {
            if (specArguments.ContainsKey("key"))
                SetKey(specArguments["key"]);
            if (specArguments.ContainsKey("rounds"))
                _rounds = Convert.ToUInt32(specArguments["rounds"]);

            return true;
        }

        public byte[] Crypt(byte[] input)
        {

            // Splitting the 128-bit key into four 32-bit values
            var keyBuffer = new[]
            {
                BitConverter.ToUInt32(_key, 0), BitConverter.ToUInt32(_key, 4), BitConverter.ToUInt32(_key, 8),
                BitConverter.ToUInt32(_key, 12)
            };

            // Array that contains two 32-bit values
            var blockBuffer = new uint[2];

            // Initializing array with padding bytes to make all blocks 64 bits
            var result = new byte[(((input.Length + 4) >> 3) + 1) << 3];
            var lengthBuffer = BitConverter.GetBytes(input.Length);
            Array.Copy(lengthBuffer, result, lengthBuffer.Length);
            Array.Copy(input, 0, result, lengthBuffer.Length, input.Length);

            // Main loop
            using (var stream = new MemoryStream(result))
            {
                using (var writer = new BinaryWriter(stream))
                {
                    // i increases by 8 because every block should be 64 bits
                    for (int i = 0; i < result.Length; i += 8)
                    {
                        // First 32 bits of the block into v0, second 32 bits into v1
                        blockBuffer[0] = BitConverter.ToUInt32(result, i);
                        blockBuffer[1] = BitConverter.ToUInt32(result, i + 4);
                        Crypt(blockBuffer, keyBuffer);
                        writer.Write(blockBuffer[0]);
                        writer.Write(blockBuffer[1]);
                    }
                }
            }

            return result;
        }

        public byte[] Decrypt(byte[] output)
        {
            // If the encrypted byte array is not divisible by 8 it cannot be decrypted
            if(output.Length % 8 != 0) throw new ArgumentException("Encrypted data length must be multiple of 8 bytes for XTEA.");

            // Splitting the 128-bit key into four 32-bit values
            var keyBuffer = new[]
            {
                BitConverter.ToUInt32(_key, 0), BitConverter.ToUInt32(_key, 4), BitConverter.ToUInt32(_key, 8),
                BitConverter.ToUInt32(_key, 12)
            };

            // Array that contains two 32-bit values
            var blockBuffer = new uint[2];

            // Placing the byte array into a buffer
            var buffer = new byte[output.Length];
            Array.Copy(output, buffer, output.Length);

            using (var stream = new MemoryStream(buffer))
            {
                using (var writer = new BinaryWriter(stream))
                {
                    for (int i = 0; i < buffer.Length; i += 8)
                    {
                        blockBuffer[0] = BitConverter.ToUInt32(buffer, i);
                        blockBuffer[1] = BitConverter.ToUInt32(buffer, i + 4);
                        Decrypt(blockBuffer, keyBuffer);
                        writer.Write(blockBuffer[0]);
                        writer.Write(blockBuffer[1]);
                    }
                }
            }

            // Verifying if length is valid
            var length = BitConverter.ToUInt32(buffer, 0);
            if(length > buffer.Length - 4) throw new ArgumentException("Invalid encrypted data");
            var result = new byte[length];
            Array.Copy(buffer, 4, result, 0, length);

            return result;
        }

        #endregion

    }
}
