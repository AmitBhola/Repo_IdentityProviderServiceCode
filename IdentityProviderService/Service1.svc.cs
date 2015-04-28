using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EncryptionAndEncodingHelper;
using System.Data.SqlClient;

namespace IdentityProviderService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private EncryptionAndEncoding eeh = new EncryptionAndEncoding("0123456789ABCDEF", "ABCDEFGH");
        private string connection  = "Data Source=AMIT-WIN;Initial Catalog=FinalPolicyDb;Integrated Security=True";
        private string userNickName = String.Empty;
        private string userAddr = String.Empty;
        private string userName = String.Empty;
        public string GetData(String value)
        {
            string usernameAndPassword = eeh.DecryptAndDecodeText(value);
            string username = usernameAndPassword.Split(':')[0];
            string password = usernameAndPassword.Split(':')[1];
            password = password.Substring(0, 3);
            if(Validate(username,password)){
                return eeh.EncryptAndEncodeText(string.Format("Valid"));
            }
            else{
                return eeh.EncryptAndEncodeText(string.Format("The Password you entered: Is not Valid"));
            }


        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            string usernameAndPassword = eeh.DecryptAndDecodeText(composite.EncryptedAndEncodedText);
            string username = usernameAndPassword.Split(':')[0];
            string password = usernameAndPassword.Split(':')[1];
            password = password.Substring(0, 3);
            composite.BoolValue = false;
            if (Validate(username, password))
            {
                composite.BoolValue = true;
                composite.UserName = userName;
                composite.UserAddr = userAddr;
                composite.UserNickName = userNickName;
            }
            
            return composite;
        }

        protected Boolean Validate(string username, string password)
        {
            try
            {

                SqlConnection sqlConnection1 = new SqlConnection("Data Source=AMIT-WIN;Initial Catalog=FinalPolicyDb;Integrated Security=True");
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                string user = string.Empty;

                cmd.CommandText = "sp_auth";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@user", System.Data.SqlDbType.VarChar, 255).Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 255).Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.Add("@sql_status", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.InputOutput;

                cmd.Parameters["@user"].Value = username;
                cmd.Parameters["@password"].Value = password;
                cmd.Parameters["@sql_status"].Value = 0;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    userName = reader["User_Name"].ToString();
                    userNickName = reader["User_NickName"].ToString();
                    userAddr = reader["User_Addr"].ToString();
                }
                if (Convert.ToInt32(cmd.Parameters["@sql_status"].Value) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception e)
            {
                return false;
            }
        }
                    

    }
}
