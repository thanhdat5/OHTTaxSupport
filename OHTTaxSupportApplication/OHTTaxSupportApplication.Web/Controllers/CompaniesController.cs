using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OHTTaxSupportApplication.Web.Controllers
{
    public class CompaniesController : Controller
    {
        // GET: Companies
        public ActionResult Index()
        {
            return View();
        }
    }
}