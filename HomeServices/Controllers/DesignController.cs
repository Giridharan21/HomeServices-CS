using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeServices.Controllers
{
    public class DesignController : Controller
    {
        // GET: Design
        public ActionResult Header()
        {
            return View();
        }
        public ActionResult Footer() {
            return View();
        }
    }
}