using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace IdentityProviderService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(String value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string userName = "Hello ";
        string userAddr = "";
        string userNickName = "";
        string encryptedAndEncodedText = "";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [DataMember]
        public string UserNickName
        {
            get{return userNickName;}
            set { userNickName = value; }
        }

        [DataMember]
        public string UserAddr
        {
            get{return userAddr;}
            set { userAddr = value; }
        }
        [DataMember]
        public string EncryptedAndEncodedText
        {
            get{return encryptedAndEncodedText;}
            set { encryptedAndEncodedText = value; }
        }
    

    
    }
}
