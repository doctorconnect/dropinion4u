﻿using DataAccess;
using dropinion4u.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace dropinion4u.Controllers
{
    public class HomeController : Controller
    {
        private WebPartsDataAccess objWebPartsDataAccess;
        private DirectoryDataAccess objDirectoryDataAccess;
        private SendEmail objSendEmail;
        public HomeController()
        {
            objDirectoryDataAccess = new DirectoryDataAccess();
            objWebPartsDataAccess = new WebPartsDataAccess();
            objSendEmail = new SendEmail();
        }
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult Index()
        {
           
            #region -----------------------------------  PRODUCT XML BIND SET----------------------------

            var doc = XDocument.Load(Request.PhysicalApplicationPath + "\\App_Data\\products.xml");
            var results = doc.Descendants("Table").Select(x => new
            {
                name = (string)x.Element("NAME"),
                NEVURL = (string)x.Element("NEVURL"),
                controller = (string)x.Element("controller")
            }).ToList();

            List<urllink> l3 = new List<urllink>();

            for (int i = 0; i < results.Count; i++)
            {
                urllink j1 = new urllink();
                j1.NAME = results[i].name;
                j1.action1 = results[i].NEVURL;
                j1.control1 = results[i].controller;
                l3.Add(j1);
            }

            ViewBag.listdata = l3;

            #endregion   -------------------------------- END OF PRODUCT XML DATA BIND --------------------------------

            #region -----------------------------------  SOLUTION XML BIND SET----------------------------

            var doc1 = XDocument.Load(Request.PhysicalApplicationPath + "\\App_Data\\solutions.xml");
            var results1 = doc1.Descendants("node").Select(x => new
            {
                description = (string)x.Element("description"),
                NEVURL1 = (string)x.Element("NEVURL"),
                controller = (string)x.Element("controller")
            }).ToList();

            List<urllinksolution> l4 = new List<urllinksolution>();
            for (int i = 0; i < results1.Count; i++)
            {
                urllinksolution j1 = new urllinksolution();
                j1.NAME = results1[i].description;
                j1.action1 = results1[i].NEVURL1;
                j1.control1 = results1[i].controller;
                l4.Add(j1);
            }

            ViewBag.solution = l4;

            #endregion   -------------------------------- END OF SOLUTION XML DATA BIND --------------------------------

            ViewData["ViewResult"] = results;
            ViewBag.data = results;
            return View();
        }

        public ActionResult UserProfile()
        {
            return View();
        }

        public ActionResult LoadPostsAndCommentPartialView()
        {
            ViewBag.PostList = objDirectoryDataAccess.GetPost();
            return PartialView("~/Views/SelfProfile/_MyPostAndComment.cshtml");
        }

        public ActionResult Loadlikecount()
        {
            string keyId = Request.QueryString["key"];
            ViewBag.PostList = objDirectoryDataAccess.GetPost();
            return PartialView("~/Views/SelfProfile/_likeCount.cshtml", new ViewDataDictionary { { "cpid", keyId } });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SavePost(string Message)
        {
            var msg = objDirectoryDataAccess.SubmitPost(Message.Replace("!!", "&").Replace("!!!", "'\'"));
            ViewBag.PostList = objDirectoryDataAccess.GetPost();
            return PartialView("~/Views/SelfProfile/_MyPostAndComment.cshtml");
        }

        [HttpPost]
        public ActionResult SaveComment(int PostId, string txtcomment, string Identifier)
        {
            int msg = 0;
            msg = objDirectoryDataAccess.SubmitCommentt(PostId, txtcomment, Identifier);
          if (Identifier == "POST")
            {
                ViewBag.PostList = objDirectoryDataAccess.GetPost();
                return PartialView("~/Views/SelfProfile/_MyPostAndComment.cshtml");
            }            
            return RedirectToAction("Index", "Home");
        }
        public ActionResult PostLikes()
        {
            int keyId = int.Parse(Request.QueryString["key"]);
            string Identifier = Request.QueryString["Identifier"];
            try
            {
                int msg = 0;
                msg = objDirectoryDataAccess.SubmitLike(keyId, Identifier);
                if (msg > 0)
                    TempData["success"] = "Post Liked";
                else
                    TempData["error"] = "Some Error Occured. Please Contact Admin";

            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (Identifier == "POST")
            {
                ViewBag.PostList = objDirectoryDataAccess.GetPost();
                return PartialView("~/Views/SelfProfile/_MyPostAndComment.cshtml");
            }            
            return RedirectToAction("Index", "Home");
        }

        public ActionResult FlagPost()
        {
            string keyId = Request.QueryString["key"];
            string Id = Request.QueryString["Id"].PadLeft(9, '0');
            string Identifier = Request.QueryString["Identifier"];
            try
            {
                int msg = 0;
                msg = objDirectoryDataAccess.PostFlag(int.Parse(keyId));
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (Identifier == "POST")
            {
                ViewBag.PostList = objDirectoryDataAccess.GetPost();
                return PartialView("~/Views/SelfProfile/_MyPostAndComment.cshtml");
            }
            return RedirectToAction("Index", "Home");
        }

    }
}