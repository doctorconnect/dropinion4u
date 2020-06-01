using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dropinion4u.Controllers
{
    public class LoginController : Controller
    {
        private LogindataAccess objLogindataAccess;        

        public LoginController()
        {
            objLogindataAccess = new LogindataAccess();
            
        }
        // GET: Login
        public ActionResult Signin(string email)
        {
            if(email != null)
            {
                Session["email"] = email;
                var userDetails = objLogindataAccess.GetListOfRegisteredUser().Where(x => x.UserEmail == email).FirstOrDefault();
                if(userDetails == null)
                {
                    objLogindataAccess.SubmitUserRequest(email);
                }                
                return View("SigninVerify");
            }

            return View();
        }
        public ActionResult SigninVerify(string email ,string Pass)
        {
            Session["email"] = email;
            if(email == null || email == "" )
            {
                return View("Signin");
            }
           
                var userDetails = objLogindataAccess.GetListOfRegisteredUser().Where(x => x.UserEmail == email && x.UserPassword == Pass).FirstOrDefault();
                if (userDetails != null)
                {
                    Session["UserID"] = userDetails.UserID;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var userDetailsLogin = objLogindataAccess.GetListOfRegisteredUser().Where(x => x.UserEmail == email).FirstOrDefault();

                    if (userDetailsLogin.UserPassword == "")
                    {
                        TempData["P_error"] = "Please Register Your Self. !!!";
                    }
                    else
                    {
                        TempData["P_error"] = "Password is incorrect !!!";
                    }

                }
          
            return View();
        }

        public ActionResult RegisterUser(string email, string Pass)
        {
            int msg = 0;
            var userDetails = objLogindataAccess.GetListOfRegisteredUser().Where(x => x.UserEmail == email).FirstOrDefault();
            if (userDetails == null)
            {
                msg = objLogindataAccess.SubmitUserRequest(email, Pass);
                TempData["registerSuccess"] = "User Is Registered Successfully";
            }
            else
            {
                TempData["userExist"] = "User Already Exist.";
            }
            Session["UserID"] = "";
            return View();
        }
    }
}