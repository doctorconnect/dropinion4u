using BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;

namespace DataAccess
{
    public class LogindataAccess
    {
        Database m_Database;
        UserRegistrationModel objUserRegistrationModel;
        
        public LogindataAccess(string key = null)
        {
            string connectionkey = GetconnectionKey(key);
            // ObjDirectoryDataAccess = new WebPartsDataAccess(); 
            m_Database = DatabaseFactory.CreateDatabase(connectionkey);
        }
        private string GetconnectionKey(string KEY)
        {
            string connectionkey = string.Empty;

            if (HttpContext.Current.Request.Url.ToString().Contains("dropinion"))
            {
                connectionkey = DBConstants.Connectstring;
            }
            else if (HttpContext.Current.Request.Url.ToString().Contains("44327"))
            {

                connectionkey = DBConstants.LocConnectB;
            }
            else
            {
                connectionkey = DBConstants.LocConnectV;
            }
            return connectionkey;

        }
        public List<UserRegistrationModel> GetListOfRegisteredUser()
        {
            List<UserRegistrationModel> objUserRegistrationModel = new List<UserRegistrationModel>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstants.PROCGETLISTOFREGISTEREDUSER))
            {
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objUserRegistrationModel.Add(GetUserRegistrationDetailsFromDataReader(dataReader));
                    }
                }
            }
            return objUserRegistrationModel;
        }

        public int SubmitUserRequest(string UserEmail, string UserPassword = null)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstants.SUBMITUSERDETAILS))
            {               
                m_Database.AddInParameter(dbCommand, "@Email", DbType.String, UserEmail);
                m_Database.AddInParameter(dbCommand, "@Password", DbType.String, UserPassword);                

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int UpdateUserRequest(string UserEmail, string UserPassword)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstants.UPDATEUSERDETAILS))
            {
                m_Database.AddInParameter(dbCommand, "@Email", DbType.String, UserEmail);
                m_Database.AddInParameter(dbCommand, "@Password", DbType.String, UserPassword);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }






        private UserRegistrationModel GetUserRegistrationDetailsFromDataReader(IDataReader datareader)
        {
            objUserRegistrationModel = new UserRegistrationModel();
            objUserRegistrationModel.UserID = SafeTypeHandling.ConvertStringToInt32(datareader["Id"]);
            objUserRegistrationModel.UserPassword = SafeTypeHandling.ConvertToString(datareader["Password"]);
            objUserRegistrationModel.UserEmail = SafeTypeHandling.ConvertToString(datareader["Email"]);
            
            return objUserRegistrationModel;
        }
    }
}
