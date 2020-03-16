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
                return Redirect("~/Login/Login");
            }
            return View();
        }
        

        public ActionResult ServiceProvider()
        {
            SPRegisterClass NewRegister = new SPRegisterClass();
            NewRegister.ServiceList = Data.GetServices();
            return View(NewRegister);
           
        }
        [HttpPost]
        public ActionResult ServiceProvider(SPRegisterClass mom) {
            SPRegisterClass NewRegister = new SPRegisterClass();
            NewRegister.ServiceList = Data.GetServices();
            if (ModelState.IsValid)
            {
                Data.registeruser(mom);
                return Redirect("~/Login/Login");
            }
            return View(NewRegister);
        }






    }
}