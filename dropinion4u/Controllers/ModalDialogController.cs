using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessEntities;
using DataAccess;
using dropinion4u.Models;

namespace dropinion4u.Controllers
{
    public class ModalDialogController : Controller
    {
        private LogindataAccess objLogindataAccess;
        private WebPartsDataAccess objWebPartsDataAccess;
        private SendEmail objSendEmail;
        public ModalDialogController()
        {
            objLogindataAccess = new LogindataAccess();
            objWebPartsDataAccess = new WebPartsDataAccess();
            objSendEmail = new SendEmail();

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
        public ActionResult _VerificationForm()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Feedback(string Feedback)
        {
            try
            {
                objWebPartsDataAccess.SubmitFeedBack(Feedback);
                return Json("Feedback Submmit Successfully");
            }
            catch
            {
                return Json("Error");
            }
            //  return new JsonResult { response = ("Feedback Submmit Successfully") };
        }

        public JsonResult CheckEmail(string email)
        {
            try
            {
                if (email != null && email != "")
                {
                    Session["email"] = email;
                    var userDetails = objLogindataAccess.GetListOfRegisteredUser().Where(x => x.UserEmail == email).FirstOrDefault();
                    if (userDetails == null)
                    {
                        string pass = "xytxmshivnandan123456dropinior";
                        objLogindataAccess.SubmitUserRequest(email, pass);
                        TempData["success"] = "Submit User Request sucess ";
                        return new JsonResult { Data = ("Register", email) };
                    }
                    else
                    {
                        var userDetailsLogin = objLogindataAccess.GetListOfRegisteredUser().Where(x => x.UserEmail == email).FirstOrDefault();

                        if (userDetailsLogin.UserPassword == "xytxmshivnandan123456dropinior")
                        {
                            return new JsonResult { Data = ("Register", email) };
                        }
                        else
                        {
                            return new JsonResult { Data = ("Login", email) };
                        }
                    }
                }
                else
                {
                    return new JsonResult { Data = ("LoginFailed", "EmailMissing") };
                }
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ("Error",ex+ "Something Wrong at our side...") };
            }
        }
        public JsonResult Login(string Pass)
        {
            try
            {
                string email = Session["email"].ToString();
                var useex = objLogindataAccess.GetUserExist(email, Pass);
                var userDetails = objLogindataAccess.GetListOfRegisteredUser().Where(x => x.UserEmail == email && x.UserPassword == Pass).FirstOrDefault();
                if (userDetails != null)
                {
                    Session["UserID"] = userDetails.UserID;
                    Session["EmailVerified"] = false;
                    return new JsonResult { Data = ("LoginSuccessful", Session["UserID"]) };
                }
                else
                {
                    var userDetailsLogin = objLogindataAccess.GetListOfRegisteredUser().Where(x => x.UserEmail == email).FirstOrDefault();
                    TempData["P_error"] = "Password is incorrect !!!";
                    return new JsonResult { Data = ("LoginFailed", "IncorrectPassword") };
                }
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ("Error",ex+ "Something Wrong at our side...") };
            }
        }
        public JsonResult Register(string Pass)
        {
            try
            {
                string email = Session["email"].ToString();
                int msg = 0;
                var userDetails = objLogindataAccess.GetListOfRegisteredUser().Where(x => x.UserEmail == email).FirstOrDefault();
                if (userDetails != null)
                {
                    UserRegistrationModel user = new UserRegistrationModel();
                    user.RoleId = 12;
                    user.UserCode = RandomDigits(8);
                    user.UserNTID = "HUB" + RandomDigits(4);
                    user.UserName = "GUEST";
                    user.UserEmail = email;
                    user.ManagerName = "INDIA";
                    user.ManagerNTID = Pass;
                    user.ManagerCode = Pass;
                    user.ManagerEmail = email;
                    user.BusinessSegmentId = 1;
                    user.CapabilitiesId = 1;
                    user.LOBId = 1;
                    user.Status = 6;
                    user.AboutMe = "Describe yourself in 140 characters";


                    var imagPath = Server.MapPath("~/images/avtar.png");
                    Image img = Image.FromFile(imagPath);
                    byte[] imageDatabytes = (byte[])(new ImageConverter()).ConvertTo(img, typeof(byte[]));
                   
                    msg = objLogindataAccess.SubmitUserRequest(null, imageDatabytes, user);
                    
                    //msg = objLogindataAccess.UpdateUserRequest(email, Pass);
                    Session["UserID"] = userDetails.UserID;
                    return new JsonResult { Data = ("RegistrationSuccessful", Session["UserID"]) };
                }
                return new JsonResult { Data = ("RegistrationFailed", "Something Fishy...") };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ("Error",ex+ "Something Wrong at our side...") };
            }
        }

        public JsonResult RequestOTP(string email)
        {
            Random generator = new Random();
            Session["Otp"] = generator.Next(0, 1000000).ToString("D6");
            string x = "Dear User,<br/><br/>Greetings from Dr.Opinion.<br/><br/>Your OTP is " + Session["Otp"] + "<br/><br/>Thanks<br/>The Dr.Opinion Team";
            objSendEmail.SendPasswordToEmail(email, null, "[Dr.Opinion] OTP Details ", x);
            return new JsonResult { Data = ("OTPSent", email) };

        }

        public JsonResult VerifyOTP(string otp,string email ,string pass)
        {
            if(Session["Otp"].ToString() == otp)
            {
                var userDetails = objLogindataAccess.GetListOfRegisteredUser().Where(x => x.UserEmail == email).FirstOrDefault();
                if (userDetails != null)
                {
                    objLogindataAccess.UpdateUserRequest(email, pass);
                    Session["UserID"] = userDetails.UserID;
                    return new JsonResult { Data = ("VerifyOTPSuccessful", Session["UserID"]) };
                }
            }           

            return new JsonResult { Data = ("InvalidOTP", "Invalid Otp") };

        }

        public JsonResult Logout()
        {
            Session["UserID"] = null;
            FormsAuthentication.SignOut();           
            return new JsonResult { Data = ("LogoutSuccessful", Session["UserID"]) };
        }

        public string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
    }
    }