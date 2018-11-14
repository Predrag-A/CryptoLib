using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptoLib
{
    public class DoubleTransposition: ICrypto
    {

        #region Fields

        // Key represented by two strings
        private string[] _key;

        // Temporary matrix to use for transposition
        private char[][] _matrix;

        // Array containing alphanumeric characters for generating random keys
        private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        #endregion

        #region Constructors

        public DoubleTransposition()
        {
            SetKey(GenerateRandomKey());
;        }

        #endregion

        #region Methods

        // Function that swaps columns of a jagged array matrix
        public static void SwapColumns(int col1, int col2, char[][] matrix)
        {
            foreach (var t in matrix)
            {
                var temp = t[col1];
                t[col1] = t[col2];
                t[col2] = temp;
            }
        }

        #endregion

        #region Interface Methods

        // Key is transmited in the following format: ColumnKey,RowKey
        public bool SetKey(byte[] input)
        {
            var temp = Encoding.ASCII.GetString(input);
            var keyArray = temp.Split(',');
            if (keyArray.Length != 2) return false;
            _key = keyArray;
            return true;
        }

        public byte[] GenerateRandomKey()
        {
            var rand = new Random();
            var k1 = new string(Enumerable.Repeat(CHARS, rand.Next(20)).Select(s => s[rand.Next(s.Length)]).ToArray());
            var k2 = new string(Enumerable.Repeat(CHARS, rand.Next(20)).Select(s => s[rand.Next(s.Length)]).ToArray());
            var key = string.Join(",", k1, k2);
            return Encoding.ASCII.GetBytes(key);
        }

        // Double Transposition does not use an initialization vector
        public bool SetIV(byte[] input)
        {
            throw new NotImplementedException();
        }

        public byte[] GenerateRandomIV()
        {
            throw new NotImplementedException();
        }

        // Double Transposition has no properties to set
        public bool SetAlgorithmProperties(IDictionary<string, byte[]> specArguments)
        {
            throw new NotImplementedException();
        }


        public byte[] Crypt(byte[] input)
        {
            // Get input as string without spaces
            var inputString = Encoding.ASCII.GetString(input).Replace(" ", string.Empty).ToUpper();

            // Initialize StringBuilder object to contain output string
            var sb = new StringBuilder();

            // Initialize rows and columns
            var rows = _key[0].Length;
            var columns = _key[1].Length;

            // Size of matrix to determine block size
            var matrixSize = rows * columns;
            
            // Determining number of blocks
            var blockNum = (inputString.Length % matrixSize == 0) ? inputString.Length/matrixSize : inputString.Length/matrixSize + 1;
            

            // Main loop
            for (var i = 0; i < blockNum; i++)
            {
                var colKey = _key[0].ToCharArray();
                var rowKey = _key[1].ToCharArray();

                // Initialize matrix
                _matrix = new char[rows][];
                for (var j = 0; j < rows; j++)
                    _matrix[j] = new char[columns];

                // Forming matrix
                for (var j = 0; j < rows; j++)
                for (var k = 0; k < columns; k++)
                {
                    var index = i * matrixSize + j * rows + k;
                    if (index > inputString.Length - 1) break;
                    _matrix[j][k] = inputString[index];
                }

                // Sorting key and transposing by columns
                for (var j = 0; j < columns - 1; j++)
                {
                    var min = j;

                    for (var index = j + 1; index < columns; index++)
                        if (colKey[index] < colKey[min])
                            min = index;
                    if (min == j) continue;
                    
                    // Swapping key characters
                    var temp = colKey[j];
                    colKey[j] = colKey[min];
                    colKey[min] = temp;

                    // Swaping columns
                    SwapColumns(j, min, _matrix);
                }

                // Sorting key and transposing by rows
                for (var j = 0; j < rows - 1; j++)
                {
                    var min = j;
                    for(var index = j + 1; index < rows; index++)
                        if (rowKey[index] < rowKey[min])
                            min = index;
                    if (min == j) continue;

                    // Swapping key characters
                    var tempChar = rowKey[j];
                    rowKey[j] = rowKey[min];
                    rowKey[min] = tempChar;

                    // Swapping rows
                    var tempRow = _matrix[j];
                    _matrix[j] = _matrix[min];
                    _matrix[min] = tempRow;
                }

                // Forming output string
                for (var j = 0; j < rows; j++)
                for (var k = 0; k < columns; k++)
                    sb.Append(_matrix[j][k]);
            }

            var outputString = sb.ToString();

            return Encoding.ASCII.GetBytes(outputString.Replace("\0", " "));
        }

        public byte[] Decrypt(byte[] output)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
