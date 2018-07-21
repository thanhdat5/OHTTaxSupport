using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OHTTaxSupportApplication.Web.Controllers
{
    public class InvoicesController : Controller
    {
        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }
    }
}