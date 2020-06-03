using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dropinion4u.Controllers
{
    public class SupportController : Controller
    {
        private WebPartsDataAccess objWebPartsDataAccess;
        public SupportController()
        {
            objWebPartsDataAccess = new WebPartsDataAccess();
        }
        // GET: Support
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Privacy()
        {
            ViewBag.Message = "Your privacy page.";

            return View();
        }

        [HttpPost]
        public ActionResult Feedback(string Feedback)
        {
            objWebPartsDataAccess.SubmitFeedBack(Feedback);
            ViewBag.message = "Submmit Feedback";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult FAQs()
        {
            ViewBag.Message = "Your FAQs page.";

            return View();
        }
    }
}