using System;
using System.IO;

namespace CryptoApp.Classes
{
    /// <inheritdoc />
    /// Notice : Code written by Dimitris Papadimitriou - http://www.papadi.gr
    /// Code is provided to be used freely but without any warranty of any kind
    public class ProgressStream : Stream
    {

        #region Fields

        private readonly FileStream _file;
        private readonly long _length;
        private long _bytesRead;

        #endregion

        #region Helper Classes

        public class ProgressChangedEventArgs : EventArgs
        {
            public long BytesRead;
            public long Length;

            public ProgressChangedEventArgs(long bytesRead, long length)
            {
                BytesRead = bytesRead;
                Length = length;
            }
        }

        #endregion

        #region Events

        // Used to invoke attached method when progress changes
        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        #endregion

        #region Constructors

        public ProgressStream(FileStream file)
        {
            _file = file;
            _length = file.Length;
            _bytesRead = 0;
            ProgressChanged?.Invoke(this, new ProgressChangedEventArgs(_bytesRead, _length));
        }

        #endregion

        #region Methods

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override void Flush() { }

        public override long Length => throw new Exception("The method or operation is not implemented.");

        public override long Position
        {
            get => _bytesRead;
            set => throw new Exception("The method or operation is not implemented.");
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            // Read file
            var result = _file.Read(buffer, offset, count);

            // Increase amount of bytes read
            _bytesRead += result;

            // Invoke event handler
            ProgressChanged?.Invoke(this, new ProgressChangedEventArgs(_bytesRead, _length));
            return result;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void SetLength(long value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

    }
}
