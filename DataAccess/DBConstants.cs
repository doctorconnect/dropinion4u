using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess
{
    public class DBConstants
    {
        public const string LocConnectB = "LocConnect";        
        public const string LocConnectV = "LocConnectVinay";
        public const string Connectstring = "ServerConnect";
        public const string PROCGETRSSFEED = "uspGetRSSFeed";
        public const string PROCGETLISTOFREGISTEREDUSER = "UspGetRegister";
        public const string SUBMITUSERDETAILS = "UspSubmitDetails";
        public const string UPDATEUSERDETAILS = "Dro_UpdateUserDetails";
        public const string SUBMITFEEDBACK = "DRO_UspSubmitFeedback";
        public const string PROCSUBMITUSERREQUEST = "uspSubmitUserRequest";
        public const string PROCCHECKUSEREXISTS = "uspGetUserExists";

        //  Post  Proc
        public const string PROCGETLISTOFPOST = "uspGetListOfPost";
        public const string PROCSUBMITPOST = "uspSubmitPost";
        public const string PROCGETPOSTLIST = "uspGetPostList";
        public const string PROCDELETEPOST = "uspDeletePost";
        public const string PROCUNFLAGEPOST = "uspUnFlagFlagPost";
        public const string PROCGETPOSTLIKE = "uspGetLike";
        public const string PROCGETPOSTLIKECOUNT = "uspGetLikeCount";
        public const string PROCSUBMITPOSTFLAG = "uspSubmitPostFlag";
        // comment Proc and like proc
        public const string PROCSUBMITCOMMENT = "uspSubmitComment";
        public const string PROCSUBMITLIKE = "uspSubmitLike";


        public const string PROCGETCOMMENTLIST = "uspGetCommentList";
        public const string PROCGETCOMMENT = "uspGetComment";
    }
}