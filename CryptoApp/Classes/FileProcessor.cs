using System;
using System.Collections.Generic;
using System.Threading;

namespace CryptoApp.Classes
{
    public class FileProcessor: IDisposable
    {

        #region Fields

        private EventWaitHandle _eventWaitHandle = new AutoResetEvent(false);
        private Thread _worker;
        private readonly object _locker = new object();
        private Queue<string> _fileNamesQueue = new Queue<string>();
        private FileCypher _cypher;

        #endregion

        #region Constructors

        public FileProcessor()
        {
            _worker = new Thread(Work);
            _cypher = new FileCypher();
            
            _worker.Start();

        }

        #endregion

        #region Methods

        public void EnqueueFileName(string fileName)
        {
            // Enqueue file name and lock the queue to prevent other threads from accessing it
            lock(_locker)
                _fileNamesQueue.Enqueue(fileName);
            // Signal that the file name is enqueued and can be processed
            _eventWaitHandle.Set();
        }

        private void Work()
        {
            while (true)
            {
                string fileName = null;
                
                // Take the file name
                lock(_locker)
                    if (_fileNamesQueue.Count > 0)
                    {
                        fileName = _fileNamesQueue.Dequeue();

                        // If the queued file name is null, exit
                        if (fileName == null) return;
                    }

                if (fileName != null)
                {
                    // Waiting until file creation is done
                    while(!FileCypher.IsFileReady(fileName))
                        Thread.Sleep(50);

                    // File is sent to the cypher for encryption/decryption
                    if (Settings.Instance.FswDecrypt) _cypher.DecryptFile(fileName);
                    else _cypher.CryptFile(fileName);
                }
                else
                {
                    _eventWaitHandle.WaitOne();
                }
            }

        }

        #endregion

        #region Interface Methods

        public void Dispose()
        {
            // Signal the file processor to exit
            EnqueueFileName(null);

            // Wait for the thread to finish
            _worker.Join();
            
            // Release handle resources
            _eventWaitHandle.Close();
        }

        #endregion

    }
}
