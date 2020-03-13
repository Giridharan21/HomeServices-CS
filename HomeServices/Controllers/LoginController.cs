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
            var TypeValue = Data.Addlogin(Model);
            if (ModelState.IsValid && TypeValue!=null)
            {         
                if (TypeValue.ToLower() == "customer")
                    return RedirectToAction("Dashboard", "CustomerDashboard");
                else if (TypeValue.ToLower() == "service provider")
                    return RedirectToAction("Index", "Home");
            }

            return View();
        }

    }
}