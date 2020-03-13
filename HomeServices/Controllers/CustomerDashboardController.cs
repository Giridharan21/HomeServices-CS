using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Models;
namespace HomeServices.Controllers
{
    public class CustomerDashboardController : Controller
    {
        // GET: CustomerDashboard
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult PlaceOrder()
        {
            var result = Data.ServiceList();
            ViewBag.result = new SelectList(result);
            return View();
        }
        
        public ActionResult ServiceProvider(string service)
        {
            var Result = Data.SPList(service);
            //ViewBag.result = Result;
            return View(Result);
        }
        [HttpPost]
        public ActionResult ServiceProvider(ServiceProviderList model)
        {
            ServicesContext serObj = new ServicesContext();
            Order orderobj = new Order();
            orderobj.Date = DateTime.Now;
            orderobj.ScheduleDate = model.scheduledDate;
            orderobj.ToFK = model.ServiceProviderId;
            serObj.Orders.Add(orderobj);
            serObj.SaveChanges();
            return RedirectToAction("PreviousOrder");
        }
        public ActionResult PreviousOrder()
        {
            var Result = Data.Order(1);
            //ViewBag.result = Result;
            return View(Result);
        }
    }
}