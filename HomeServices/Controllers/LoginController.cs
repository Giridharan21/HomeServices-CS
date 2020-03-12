﻿using DataAccessLayer;
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
            if (ModelState.IsValid)
            {
                var TypeValue = Data.Addlogin(Model);

                if (TypeValue.ToLower() == "customer")
                    return RedirectToAction("Index", "Home");
                else if (TypeValue.ToLower() == "serviceprovider")
                    return RedirectToAction("Action", "Home");


                return View();
            }
            return View();
        }

    }
}