using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace dropinion4u
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
           
            string Y = Server.MachineName.ToString();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        protected void Session_Start(object sender, EventArgs e)
        {

            HttpContext.Current.Session["MachineName"] = Environment.MachineName.ToString();
            HttpContext.Current.Session["UserName"] = Environment.UserName.ToString();
            HttpContext.Current.Session["Browser"] = HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version;
            HttpContext.Current.Session["UserHostAddress"] = HttpContext.Current.Request.UserHostAddress;
            HttpContext.Current.Session["UserAgent"] = HttpContext.Current.Request.UserAgent;
            HttpContext.Current.Session["UserHostName"] = HttpContext.Current.Request.UserHostName;
            HttpContext.Current.Session["ID"] = "1";
            HttpContext.Current.Session["UserNTID"] = "HUBshiv";
            HttpContext.Current.Session["CapabilitiesId"] = "1";
            HttpContext.Current.Session["Adminstrator"] = "1";

        }
    }
}
