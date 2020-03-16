using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Models;
using DataAccessLayer;

namespace HomeServices.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult AddorRemoveServices()
        {
            return View();
        }
        public ActionResult RemoveInvalidUsers()
        {
            Data.RemoveUsers(1);
            return View();
        }
    }
}