using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Models;
using DataAccessLayer;

namespace HomeServices.Controllers
{
    public class ProviderController : Controller
    {
        // GET: Provider
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Receivedorder()
        {

            var res = Data.ServiceProvider();
            return View(res);
        }

        [HttpPost]
        public ActionResult Receivedorder(int OrderId, string Status)
        {
            Data.Accept(OrderId, Status);
            return View();
        }

        public ActionResult Previousorder()
        {
            var res = Data.ServiceProvider();
            return View(res);

        }
    }
}