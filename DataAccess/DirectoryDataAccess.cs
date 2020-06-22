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
    public class DirectoryDataAccess
    {
        Database m_Database;
        posts objPost;
        private DirectoryDataAccess ObjDirectoryDataAccess;
        public DirectoryDataAccess(string key = null)
        {
            string connectionkey = GetconnectionKey(key);
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


        public List<posts> GetLikeCount(string Identifier)
        {
            List<posts> objPost = new List<posts>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstants.PROCGETPOSTLIKE))
            {
                //  m_Database.AddInParameter(dbCommand, "@PostId", DbType.String, id);
                m_Database.AddInParameter(dbCommand, "@Identifier", DbType.String, Identifier);
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objPost.Add(GetLikeCountFromDataReader(dataReader));
                    }
                }
            }
            return objPost;
        }

        public List<posts> GetPost()
        {
            List<posts> objPost = new List<posts>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstants.PROCGETPOSTLIST))
            {
                m_Database.AddInParameter(dbCommand, "@CapabilitiesId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, 1);

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objPost.Add(GetPostFromDataReader(dataReader));
                    }
                }
            }
            return objPost;
        }

        public List<posts> GetCommentListOnPost(string Identifier)
        {
            List<posts> objPost = new List<posts>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstants.PROCGETCOMMENT))
            {
                //  m_Database.AddInParameter(dbCommand, "@PostId", DbType.String, id);
                m_Database.AddInParameter(dbCommand, "@Identifier", DbType.String, Identifier);

                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objPost.Add(GetCommentListOnPostFromDataReader(dataReader));
                    }
                }
            }
            return objPost;
        }

        public List<posts> GetListOfPost()
        {
            List<posts> objPost = new List<posts>();
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstants.PROCGETLISTOFPOST))
            {
                using (IDataReader dataReader = m_Database.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        objPost.Add(GetListOfPostFromDataReader(dataReader));
                    }
                }
            }
            return objPost;
        }

        public int SubmitPost(string msg)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstants.PROCSUBMITPOST))
            {
                m_Database.AddInParameter(dbCommand, "@PostId", DbType.Int32, "0");
                m_Database.AddInParameter(dbCommand, "@CapId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@PostedBy", DbType.Int32, HttpContext.Current.Session["ID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@Message", DbType.String, msg);
                m_Database.AddInParameter(dbCommand, "@PostedDate", DbType.DateTime, DateTime.Now);
                m_Database.AddInParameter(dbCommand, "@Remarks", DbType.String, null);
                m_Database.AddInParameter(dbCommand, "@ApproveBy", DbType.String, null);
                m_Database.AddInParameter(dbCommand, "@ApprovedDate", DbType.DateTime, null);
                m_Database.AddInParameter(dbCommand, "@Points", DbType.String, 0);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int DeleteFlagpost(string[] id)
        {
            int success = 0;
            foreach (var itm in id)
            {
                using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstants.PROCDELETEPOST))
                {
                    if (!string.IsNullOrEmpty(itm))
                    {
                        string[] item = itm.Split('^');                        
                        m_Database.AddInParameter(dbCommand, "@PostId", DbType.Int32, item[0]);
                        m_Database.AddInParameter(dbCommand, "@Identifier", DbType.String, item[1]);
                        m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);

                        success = m_Database.ExecuteNonQuery(dbCommand);
                    }
                }
            }
            return success;
        }

        public int UnFlagPost(string[] id)
        {
            int success = 0;

            foreach (var itm in id)
            {
                using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstants.PROCUNFLAGEPOST))
                {
                    if (!string.IsNullOrEmpty(itm))
                    {
                        string[] item = itm.Split('^');
                        m_Database.AddInParameter(dbCommand, "@PostId", DbType.Int32, item[0]);
                        m_Database.AddInParameter(dbCommand, "@Identifier", DbType.String, item[1]);
                        m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);

                        success = m_Database.ExecuteNonQuery(dbCommand);
                        

                    }
                }
            }
            return success;
        }

        public int SubmitCommentt(int PostId, string msg, string Identifier)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstants.PROCSUBMITCOMMENT))
            {
                m_Database.AddInParameter(dbCommand, "@PostId", DbType.Int32, PostId);
                m_Database.AddInParameter(dbCommand, "@CommentedBy", DbType.Int32, HttpContext.Current.Session["ID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@Message", DbType.String, msg);
                m_Database.AddInParameter(dbCommand, "@Identifier", DbType.String, Identifier);
                m_Database.AddInParameter(dbCommand, "@Points", DbType.String, 0);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int SubmitLike(int PostId, string Identifier)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstants.PROCSUBMITLIKE))
            {
                m_Database.AddInParameter(dbCommand, "@PostId", DbType.Int32, PostId);
                m_Database.AddInParameter(dbCommand, "@LikeBy", DbType.Int32, HttpContext.Current.Session["ID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@Identifier", DbType.String, Identifier);
                m_Database.AddInParameter(dbCommand, "@Points", DbType.String, 0);

                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        public int PostFlag(int PostId)
        {
            int success = 0;
            using (DbCommand dbCommand = m_Database.GetStoredProcCommand(DBConstants.PROCSUBMITPOSTFLAG))
            {
                m_Database.AddInParameter(dbCommand, "@PostId", DbType.Int32, PostId);
                m_Database.AddInParameter(dbCommand, "@CapId", DbType.String, HttpContext.Current.Session["CapabilitiesId"].ToString());
                m_Database.AddInParameter(dbCommand, "@FlagedBy", DbType.Int32, HttpContext.Current.Session["ID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedBy", DbType.String, HttpContext.Current.Session["UserNTID"].ToString());
                m_Database.AddInParameter(dbCommand, "@CreatedOn", DbType.DateTime, DateTime.Now);
                success = m_Database.ExecuteNonQuery(dbCommand);
            }
            return success;
        }

        private posts GetLikeCountFromDataReader(IDataReader datareader)
        {
            objPost = new posts();
            objPost.PostId = SafeTypeHandling.ConvertStringToInt32(datareader["PostId"]);
            objPost.LikeBy = SafeTypeHandling.ConvertToString(datareader["LikeBy"]);
            objPost.Identifier = SafeTypeHandling.ConvertToString(datareader["Identifier"]);

            return objPost;
        }

        private posts GetPostFromDataReader(IDataReader datareader)
        {
            objPost = new posts();
            objPost.PostId = SafeTypeHandling.ConvertStringToInt32(datareader["PostId"]);
            objPost.Message = SafeTypeHandling.ConvertToString(datareader["Message"]);
            objPost.PostedBy = SafeTypeHandling.ConvertToString(datareader["Postedby"]);
            objPost.UserCode = SafeTypeHandling.ConvertToString(datareader["UserCode"]);
            objPost.PostedByName = SafeTypeHandling.ConvertToString(datareader["PostedByName"]);
            objPost.PostedDate = SafeTypeHandling.ConvertToString(datareader["PostedDate"]);
            objPost.Status = SafeTypeHandling.ConvertToString(datareader["Status"]);
            objPost.Remarks = SafeTypeHandling.ConvertToString(datareader["Remarks"]);

            return objPost;
        }

        private posts GetListOfPostFromDataReader(IDataReader datareader)
        {
            objPost = new posts();
            objPost.PostId = SafeTypeHandling.ConvertStringToInt32(datareader["PostId"]);
            objPost.PostedBy = SafeTypeHandling.ConvertToString(datareader["Postedby"]);

            return objPost;
        }

        private posts GetCommentListOnPostFromDataReader(IDataReader datareader)
        {
            objPost = new posts();
            objPost.CommentId = SafeTypeHandling.ConvertStringToInt32(datareader["CommentId"]);
            objPost.PostId = SafeTypeHandling.ConvertStringToInt32(datareader["PostId"]);
            objPost.Message = SafeTypeHandling.ConvertToString(datareader["Message"]);
            objPost.UserCode = SafeTypeHandling.ConvertToString(datareader["UserCode"]);
            objPost.CommentedBy = SafeTypeHandling.ConvertToString(datareader["CommentedBy"]);
            objPost.CommentedByName = SafeTypeHandling.ConvertToString(datareader["CommentedByName"]);
            objPost.CommentedDate = SafeTypeHandling.ConvertToString(datareader["CommentedDate"]);
            objPost.Identifier = SafeTypeHandling.ConvertToString(datareader["Identifier"]);

            return objPost;
        }

    }
}
