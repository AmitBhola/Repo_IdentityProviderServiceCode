using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EncryptionAndEncodingHelper;

namespace IdentityProviderService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private EncryptionAndEncoding eeh = new EncryptionAndEncoding("0123456789ABCDEF", "ABCDEFGH");
        public string GetData(String value)
        {
            string username = eeh.DecryptAndDecodeText(value).Split(':')[0];
            string password = eeh.DecryptAndDecodeText(value).Split(':')[1];
            return eeh.EncryptAndEncodeText(string.Format("The Password you entered: {0}", password));
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
