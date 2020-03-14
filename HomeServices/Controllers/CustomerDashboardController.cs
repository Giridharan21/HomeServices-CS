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
        public ActionResult ViewServices()
        {
            var result = Data.ServiceList();
            ViewBag.result = new SelectList(result);
            return View();
        }
        [HttpPost]
        public ActionResult ViewServiceProvider(string Service)
        {
            var ServiceProviders = Data.SPList(Service);
            //ViewBag.result = Result;
            return View(ServiceProviders);
        }
        [HttpPost]
        public ActionResult PlaceOrder(int ServiceProviderId,DateTime ScheduleDate)
        {
            Data.PlaceOrder(ServiceProviderId, ScheduleDate);
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