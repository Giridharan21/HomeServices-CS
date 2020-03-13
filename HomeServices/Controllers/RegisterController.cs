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
        public ActionResult customer(CustomerRegisterClass dad)
        {
            if (ModelState.IsValid)
            {
                Data.AddUser(dad);
                return View();
            }
            return View();
        }
        

   public ActionResult ServiceProvider()
        {
             return View();
           
        }
            [HttpPost]
        public ActionResult ServiceProvider(SPRegisterClass mom) {
            if (ModelState.IsValid)
            {
                Data.registeruser(mom);
                return View();
            }
            return View();
        }






    }
}