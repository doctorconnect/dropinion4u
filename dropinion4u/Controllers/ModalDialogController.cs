using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using dropinion4u.Models;

namespace dropinion4u.Controllers
{
    public class ModalDialogController : Controller
    {
        private LogindataAccess objLogindataAccess;
        public ModalDialogController()
        {
            objLogindataAccess = new LogindataAccess();

        }
        // GET: ModalDialog
        public ActionResult _PostForm()
        {
            return View();
        }

        public ActionResult _LoginForm()
        {
            return View();
        }

        public ActionResult _FeedbackForm()
        {
            return View();
        }
        
        public JsonResult CheckEmail(string email)
        {
            if (email != null && email != "")
            {
                Session["email"] = email;
                var userDetails = objLogindataAccess.GetListOfRegisteredUser().Where(x => x.UserEmail == email).FirstOrDefault();
                if (userDetails == null)
                {
                    string pass = "xytxmshivnandan123456dropinior";
                    objLogindataAccess.SubmitUserRequest(email, pass);
                    return new JsonResult { Data = "Register" };
                }
                else
                {
                    var userDetailsLogin = objLogindataAccess.GetListOfRegisteredUser().Where(x => x.UserEmail == email).FirstOrDefault();

                    if (userDetailsLogin.UserPassword == "xytxmshivnandan123456dropinior")
                    {
                        return new JsonResult { Data = "Register" };
                    }
                    else
                    {
                        return new JsonResult { Data = "Login" };
                    }
                }
            }
            return new JsonResult { Data = "Register" };
        }
        public JsonResult Login(string Pass)
        {
            string email= Session["email"].ToString();
            var userDetails = objLogindataAccess.GetListOfRegisteredUser().Where(x => x.UserEmail == email && x.UserPassword == Pass).FirstOrDefault();
            if (userDetails != null)
            {
                Session["UserID"] = userDetails.UserID;
                return new JsonResult { Data = Session["UserID"] };
            }
            else
            {
                var userDetailsLogin = objLogindataAccess.GetListOfRegisteredUser().Where(x => x.UserEmail == email).FirstOrDefault();
                TempData["P_error"] = "Password is incorrect !!!";
            }
            return new JsonResult { Data = "LoginFailed" };
        }
        public JsonResult Register(string Pass)
        {
            string email = Session["email"].ToString();
            int msg = 0;
            var userDetails = objLogindataAccess.GetListOfRegisteredUser().Where(x => x.UserEmail == email).FirstOrDefault();
            if (userDetails != null)
            {
                msg = objLogindataAccess.UpdateUserRequest(email, Pass);
                Session["UserID"] = userDetails.UserID;
                return new JsonResult { Data = Session["UserID"] };
            }
            return new JsonResult { Data = Session["Registration Failed. Try later..."] };
        }
       
    }
}