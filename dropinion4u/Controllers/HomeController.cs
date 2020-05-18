using dropinion4u.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace dropinion4u.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult HomePage()
        {
            return View();
        }

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
    }
}