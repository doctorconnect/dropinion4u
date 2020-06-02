using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dropinion4u.Controllers
{
    public class TimelineController : Controller
    {
        // GET: Timeline
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _FeedPost()
        {
            return View();
        }
    }
}