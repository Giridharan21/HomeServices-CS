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
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login Model)
        {
            
            if (ModelState.IsValid && Model!=null)
            {
                UserInfoModel LoggedInUser = Data.Addlogin(Model);
                Session["UserData"] = LoggedInUser;
                if (LoggedInUser.Type == "CUSTOMER")
                    return RedirectToAction("Dashboard", "CustomerDashboard");
                else if ( LoggedInUser.Type == "SERVICE PROVIDER")
                    return RedirectToAction("Dashboard", "ServiceProvider");
            }
            ViewBag.Msg = "alert('Invalid User')";
            return View();
        }

    }
}
