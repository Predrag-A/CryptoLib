using System.Collections.Generic;
using System.ServiceModel;
using CryptoLib;

namespace CryptoWCFService
{
    #region Service Contracts

    [ServiceContract]
    public interface ICryptoService
    {

        [OperationContract]
        byte[] Crypt(byte[] input, Algorithm a);

        [OperationContract]
        byte[] DeCrypt(byte[] input, Algorithm a);

        [OperationContract]
        bool SetKey(byte[] input, Algorithm a);

        [OperationContract]
        bool SetProperties(IDictionary<string, byte[]> specArguments, Algorithm a);

        [OperationContract]
        bool SetIV(byte[] input, Algorithm a);
        
    }

    #endregion

}
