using dropinion4u.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace dropinion4u.DropinionDataAccess
{
    public class WebPartsDataAccess
    {
        Database m_Database;
        public List<RSSFeed> GetRssFeedList()
        {
            List<RSSFeed> ObjRSSFeed = new List<RSSFeed>();
            //using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstants.PROCGETRSSFEED))
            //{
            //    m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
            //    m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, HttpContext.Current.Session["Adminstrator"].ToString());
            //    using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
            //    {
            //        while (dataReader.Read())
            //        {
            //            ObjRSSFeed.Add(GetRssFeedListFromDataReader(dataReader));
            //        }
            //    }
            //}

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
    }
}