using System.ServiceModel;
using CryptoLib;

namespace CryptoWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICryptoService
    {

        #region Methods

        [OperationContract]
        byte[] Crypt(byte[] input, Algorithm a);

        [OperationContract]
        byte[] DeCrypt(byte[] input, Algorithm a);

        [OperationContract]
        bool SetKey(byte[] input, Algorithm a);

        #endregion

    }

}
