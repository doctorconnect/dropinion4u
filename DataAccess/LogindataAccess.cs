using BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
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
        private string GetconnectionKey(string KEY=null)
        {
            string connectionkey = string.Empty;
            string server = HttpContext.Current.Session["MachineName"].ToString();

            if (HttpContext.Current.Request.Url.ToString().Contains("dropinion"))
            {
                connectionkey = DBConstants.Connectstring;
            }
            else if (server == "DESKTOP-A5HEOI8")
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

        public List<UserRegistrationModel> GetRegisteredUserDetail(string UserEmail, string UserPassword)
        {
            List<UserRegistrationModel> objUserRegistrationModel = new List<UserRegistrationModel>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstants.PROCGETREGISTEREDUSERDETAILS))
            {
                m_Database.AddInParameter(dbCommand, "@Email", DbType.String, UserEmail);
                m_Database.AddInParameter(dbCommand, "@Password", DbType.String, UserPassword);
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objUserRegistrationModel.Add(GetUserDetailsFromDataReader(dataReader));
                    }
                }
            }
            return objUserRegistrationModel;
        }
        public int GetUserExist(string UserEmail, string UserPassword)
        
        {
            int success = 0;            
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstants.PROCCHECKUSEREXISTS))
            {
                m_Database.AddInParameter(dbCommand, "@Email", DbType.String, UserEmail);
                m_Database.AddInParameter(dbCommand, "@Password", DbType.String, UserPassword);

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        int Y = SafeTypeHandling.ConvertStringToInt32(dataReader["tot"]);
                    }
                }
            }
            return success;
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

        public int SubmitUserRequest(HttpPostedFileBase poImgFile, byte[] imageData, UserRegistrationModel model)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstants.PROCSUBMITUSERREQUEST))
            {
                m_Database.AddInParameter(dbCommand, "@UserCode", DbType.String, model.UserCode);
                m_Database.AddInParameter(dbCommand, "@UserNTID", DbType.String, model.UserNTID);
                m_Database.AddInParameter(dbCommand, "@UserEmail", DbType.String, model.UserEmail);
                m_Database.AddInParameter(dbCommand, "@UserName", DbType.String, model.UserName);
                m_Database.AddInParameter(dbCommand, "@ManagerCode", DbType.String, model.ManagerCode);
                m_Database.AddInParameter(dbCommand, "@ManagerEmail", DbType.String, model.ManagerEmail);
                m_Database.AddInParameter(dbCommand, "@ManagerNTID", DbType.String, model.ManagerNTID);
                m_Database.AddInParameter(dbCommand, "@ManagerName", DbType.String, model.ManagerName);
                m_Database.AddInParameter(dbCommand, "@RoleId", DbType.Int32, model.RoleId);
                m_Database.AddInParameter(dbCommand, "@BusinessSegmentId", DbType.Int32, model.BusinessSegmentId);
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.Int32, model.CapabilitiesId);
                m_Database.AddInParameter(dbCommand, "@LOBId", DbType.Int32, model.LOBId);
                m_Database.AddInParameter(dbCommand, "@Status", DbType.Int32, model.Status);
                m_Database.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, true);
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, "DOCTOR-HUB");
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@UserPhoto", DbType.Binary, imageData);
                m_Database.AddInParameter(dbCommand, "@Points", DbType.String, 0);
                if (poImgFile == null)
                {
                    m_Database.AddInParameter(dbCommand, "@AvatarExt", DbType.String, ".Png");
                }
                else
                {
                    m_Database.AddInParameter(dbCommand, "@AvatarExt", DbType.String, poImgFile.FileName.Split('\\').Last());
                }


                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }
        private UserRegistrationModel GetUserDetailsFromDataReader(IDataReader datareader)
        {
            objUserRegistrationModel = new UserRegistrationModel();
            objUserRegistrationModel.Id = SafeTypeHandling.ConvertStringToInt32(datareader["Id"]);
            objUserRegistrationModel.UserCode = SafeTypeHandling.ConvertToString(datareader["UserCode"]);
            objUserRegistrationModel.UserEmail = SafeTypeHandling.ConvertToString(datareader["UserEmail"]);
            objUserRegistrationModel.UserName = SafeTypeHandling.ConvertToString(datareader["UserName"]);
            objUserRegistrationModel.UserNTID = SafeTypeHandling.ConvertToString(datareader["UserNTID"]);
            objUserRegistrationModel.ManagerCode = SafeTypeHandling.ConvertToString(datareader["ManagerCode"]);
            objUserRegistrationModel.ManagerEmail = SafeTypeHandling.ConvertToString(datareader["ManagerEmail"]);
            objUserRegistrationModel.ManagerName = SafeTypeHandling.ConvertToString(datareader["ManagerName"]);
            objUserRegistrationModel.ManagerNTID = SafeTypeHandling.ConvertToString(datareader["ManagerNTID"]);
            objUserRegistrationModel.RoleId = SafeTypeHandling.ConvertStringToInt32(datareader["RoleId"]);
            objUserRegistrationModel.LOBId = SafeTypeHandling.ConvertStringToInt32(datareader["LOBId"]);
            objUserRegistrationModel.Status = SafeTypeHandling.ConvertStringToInt32(datareader["UserStatus"]);
            objUserRegistrationModel.StatusType = SafeTypeHandling.ConvertToString(datareader["StatusType"]);
            objUserRegistrationModel.RoleName = SafeTypeHandling.ConvertToString(datareader["RoleName"]);
            objUserRegistrationModel.LOBName = SafeTypeHandling.ConvertToString(datareader["LOBName"]);
            objUserRegistrationModel.BusinessSegmentName = SafeTypeHandling.ConvertToString(datareader["BsName"]);
            objUserRegistrationModel.CapabilitiesName = SafeTypeHandling.ConvertToString(datareader["CapName"]);
            objUserRegistrationModel.IsActive = SafeTypeHandling.ConvertStringToBoolean(datareader["IsActive"]);
            objUserRegistrationModel.AboutMe = SafeTypeHandling.ConvertToString(datareader["AboutMe"]);
            objUserRegistrationModel.CapabilitiesId = SafeTypeHandling.ConvertStringToInt32(datareader["CapabilitiesId"]);
            objUserRegistrationModel.BusinessSegmentId = SafeTypeHandling.ConvertStringToInt32(datareader["BusinessSegmentId"]);
            objUserRegistrationModel.UserPhoto = (byte[])datareader["UserPhoto"];
            objUserRegistrationModel.ImgStatus = SafeTypeHandling.ConvertStringToInt32(datareader["ImgStatus"]);
            objUserRegistrationModel.IsAdmin = SafeTypeHandling.ConvertStringToBoolean(datareader["IsAdmin"]);

            return objUserRegistrationModel;
        }

    }
}
