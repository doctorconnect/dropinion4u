using BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Security.Principal;
using System.Web;


namespace DataAccess
{
    public class WebPartsDataAccess
    {
        Database m_Database;
        private WebPartsDataAccess ObjDirectoryDataAccess;

        public WebPartsDataAccess(string key = null)
        {
            string connectionkey = GetconnectionKey(key);
             //ObjDirectoryDataAccess = new WebPartsDataAccess(); 
            m_Database = DatabaseFactory.CreateDatabase(connectionkey);            
        }
        private string GetconnectionKey(string KEY)
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
            else //if(server == "LAPTOP - S690SLSV")
            {
                connectionkey = DBConstants.LocConnectV;
            }
            return connectionkey;
           
        }
      
        public List<RSSFeed> GetRssFeedList()
        {
            List<RSSFeed> ObjRSSFeed = new List<RSSFeed>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstants.PROCGETRSSFEED))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, "1");
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, true);
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        ObjRSSFeed.Add(GetRssFeedListFromDataReader(dataReader));
                    }
                }
            }

            return ObjRSSFeed;
        }
        private RSSFeed GetRssFeedListFromDataReader(IDataReader datareader)
        {
            RSSFeed RSSFeed = new RSSFeed();
            RSSFeed.Id = Convert.ToInt32(datareader["Id"]);
            RSSFeed.Title = SafeTypeHandling.ConvertToString(datareader["Title"]);
            RSSFeed.Url = SafeTypeHandling.ConvertToString(datareader["Url"]);
            RSSFeed.IsActive = SafeTypeHandling.ConvertStringToBoolean(datareader["IsActive"]);
            RSSFeed.CreatedBy = SafeTypeHandling.ConvertToString(datareader["CreatedBy"]);
            RSSFeed.CreatedOn = SafeTypeHandling.ConvertToDateTime(datareader["CreatedOn"]);
            RSSFeed.ModifiedBy = SafeTypeHandling.ConvertToString(datareader["ModifiedBy"]);
            RSSFeed.ModifiedOn = SafeTypeHandling.ConvertToDateTime(datareader["ModifiedOn"]);

            return RSSFeed;
        }

        public int SubmitFeedBack(string FeedBack)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstants.SUBMITFEEDBACK))
            {
                m_Database.AddInParameter(dbCommand, "@Feedback", DbType.String, FeedBack);
                 success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }
    }
}
