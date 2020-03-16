using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Models;
namespace HomeServices.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index() {
            if(Session["Admin"] is null) {
                return Redirect("~/login/login");
            }
            return View();
        }
        public ActionResult AddService()
        {
            if (Session["Admin"] is null) {
                return Redirect("~/login/login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddService(string Service) {
            Data.AddService(Service);
            return Redirect("~/Admin/Index/");
        }
        public ActionResult RemoveService() {
            if (Session["Admin"] is null) {
                return Redirect("~/login/login");
            }
            ViewBag.Result = "";
            return View();
        }
        [HttpPost]
        public ActionResult RemoveService(string Service) {
            var Result = Data.RemoveService(Service);
            if (Result == 0) {
                ViewBag.Result = "alert('Service Doesn't Exist')";
                return Redirect("~/Admin/Index/");
            }
            ViewBag.Result = "alert('Service Removed Successfully')";
            return Redirect("~/Admin/Index/");

        }
    }
}