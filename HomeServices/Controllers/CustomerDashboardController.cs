using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Models;
using HomeServices.Models;
namespace HomeServices.Controllers
{
    public class CustomerDashboardController : Controller
    {
        // GET: CustomerDashboard
        public ActionResult Dashboard()
        {
            ViewBag.Login = "Customer";
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
            var Id = (UserInfoModel)Session["UserData"];
            var ServiceProviders = Data.SPList(Service,Id.Id);
            //ViewBag.result = Result;
            return View(ServiceProviders);
        }
        [HttpPost]
        public ActionResult PlaceOrder(ServiceProviderModel provider)
        {
            Data.PlaceOrder(provider);
            return RedirectToAction("MyOrders","CustomerDashboard");
        }
        public ActionResult MyOrders()
        {
            var Id = (UserInfoModel)Session["UserData"];
            var Result = Data.Order(Id.Id);
            //ViewBag.result = Result;
            ViewBag.Payment = (string)Session["Payment"];
            Session["Payment"] = null;
            return View(Result);
        }

        [HttpPost]
        public ActionResult ChangeStatus(int OrderId,string Status)
        {
            ViewBag.Payment = (string)Session["Payment"];
            Data.ChangeStatus(OrderId, Status);
            return RedirectToAction("MyOrders", "CustomerDashboard");
        }
        private int SetLogInInfo(string type) {
            if (type == "Customer") {
                ViewBag.Login = "Customer";
            }
            return 1;
        }
    }
}