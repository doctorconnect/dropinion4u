using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dropinion4u.Controllers
{
    public class ModalDialogController : Controller
    {
        // GET: ModalDialog
        public ActionResult _PostForm()
        {
            return View();
        }

        public ActionResult _LoginForm()
        {
            return View();
        }
    }
}