using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OHTTaxSupportApplication.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {    
            return View();
        }
                   
        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session["UserLogged"] = null;
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Login");
        }
    }
}