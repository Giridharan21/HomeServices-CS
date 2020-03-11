using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Models;
using DataAccessLayer;
namespace HomeServices.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            RegisterClass reg = new RegisterClass();
            reg.Type = new SelectList(new List<string> { "Customer", "Service Provider" });
            return View(reg);
        }
        [HttpPost]
        public ActionResult Index(RegisterClass reg)
        {
            reg.Type = new SelectList(new List<string> { "Customer", "Service Provider" });
            Data.adduser();
            return View(reg);
        }






    }
}