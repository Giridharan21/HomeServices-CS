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
        public ActionResult customer()
        {
        
            return View();
        }
        [HttpPost]
        public ActionResult customer(CustomerRegisterClass m)
        {if (ModelState.IsValid)
            {
                Data.AddUser(m);
                return View();
            }
            return View();
        }
        

   public ActionResult ServiceProvider()
        {
             return View();
           
        }
            [HttpPost]
        public ActionResult ServiceProvider(SPRegisterClass n) {
            if (ModelState.IsValid)
            {
                Data.registeruser(n);
                return View();
            }
            return View();
        }






    }
}