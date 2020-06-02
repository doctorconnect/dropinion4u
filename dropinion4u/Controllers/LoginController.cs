using BusinessEntities;
using DataAccess;
using dropinion4u.Models;
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

        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signin(string email)
        {
            if(email != null && email != "" )
            {
                Session["email"] = email;
                var userDetails = objLogindataAccess.GetListOfRegisteredUser().Where(x => x.UserEmail == email).FirstOrDefault();
                if(userDetails == null)
                {
                    string pass = "xytxmshivnandan123456dropinior";
                    objLogindataAccess.SubmitUserRequest(email, pass);
                    return View("SigninWithRegister");
                }
                else
                {
                    var userDetailsLogin = objLogindataAccess.GetListOfRegisteredUser().Where(x => x.UserEmail == email).FirstOrDefault();

                    if (userDetailsLogin.UserPassword == "xytxmshivnandan123456dropinior")
                    {
                        return View("SigninWithRegister"); ;
                    }
                    else
                    {
                        return View("SigninVerify");
                    }
                    
                }
               
            }

            return View();
        }
        public ActionResult SigninWithRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SigninWithRegister(PersonModel user ,string email)
        {
            int msg = 0;
            var userDetails = objLogindataAccess.GetListOfRegisteredUser().Where(x => x.UserEmail == email).FirstOrDefault();
            if (userDetails != null)
            {
                msg = objLogindataAccess.UpdateUserRequest(email, user.ConfirmPassword);
                // Session["UserID"] = userDetails.UserID;
                TempData["LoginAgain"] = "Registration Success Login Again to Access!!!";
                return RedirectToAction("Index", "Home");
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
                                  
                     TempData["P_error"] = "Password is incorrect !!!";                    

                }
          
            return View();
        }

       
    }
}