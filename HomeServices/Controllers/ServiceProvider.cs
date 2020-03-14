using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Models;
using DataAccessLayer;

namespace HomeServices.Controllers
{
    public class ServiceProvider : Controller
    {
        // GET: sp
        public ActionResult DashBoard()
        {
            return View();
        }
        public ActionResult ReceivedOrder()
        {

            var res=Data.ServiceProvider();
            return View(res);
        }

        [HttpPost]
        public ActionResult ReceivedOrder(int OrderId,string Status)
        {
            Data.Accept(OrderId,Status);
            return View();
        }

        public ActionResult PreviousOrder()
        {
            var res = Data.ServiceProvider();
            return View(res);
           
        }
    }
}