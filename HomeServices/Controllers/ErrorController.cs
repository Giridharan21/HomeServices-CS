using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeServices.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult HTTP404()
        {
            return View();
        }
    }
}