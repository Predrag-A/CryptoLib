using System;
using System.ServiceModel;

namespace CryptoWCFService
{

    #region Service Contracts

    [ServiceContract]
    public interface ICloudService
    {
        [OperationContract]
        UploadReply UploadFile(RemoteFileInfo request);

        [OperationContract]
        RemoteFileInfo DownloadFile(DownloadRequest request);

        [OperationContract]
        string[] GetFileList();

        [OperationContract]
        string GetKey();

        [OperationContract]
        bool DeleteFile(string fileName);
        
    }

    #endregion

    #region Message Contracts

    [MessageContract]
    public class DownloadRequest
    {
        [MessageBodyMember] public string FileName;
    }

    [MessageContract]
    public class UploadReply
    {
        [MessageBodyMember] public string FileName;
    }

    [MessageContract]
    public class RemoteFileInfo : IDisposable
    {
        [MessageHeader(MustUnderstand = true)] public string FileName;

        [MessageHeader(MustUnderstand = true)] public long Length;

        [MessageBodyMember(Order = 1)] public System.IO.Stream FileByteStream;

        public void Dispose()
        {
            // Close stream when the contract instance is disposed. This ensures that
            // stream is closed when file download is complete, since download procedure
            // is handled by the client and the stream must be closed on server.
            if (FileByteStream == null) return;
            FileByteStream.Close();
            FileByteStream = null;
        }
    }

    #endregion

}

