using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Models;

namespace HomeServices.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            ViewBag.Login = "NotLoggedIn";
            Session["Admin"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login Model)
        {
            if (ModelState.IsValid && Model!=null)
            {
                if (Model.UserName=="admin"&&Model.Password=="admin123") {
                    Session["Admin"] = "admin";
                    return Redirect("~/Admin/Index");
                }
               Session["Admin"] = null;
                UserInfoModel LoggedInUser = Data.Addlogin(Model);
                Session["UserData"] = LoggedInUser;
                if (LoggedInUser.Type == "CUSTOMER") {
                    return RedirectToAction("Dashboard", "CustomerDashboard");
                }
                else if (LoggedInUser.Type == "SERVICE PROVIDER") {
                    ViewBag.Login = "ServiceProvider";
                    return RedirectToAction("Dashboard", "Provider");
                }
                else { 
                    ViewBag.Login = "Invalid User";
                    return View();
                }
            }
            ViewBag.Login = "LoginFailed";
            return View();
        }

    }
}
