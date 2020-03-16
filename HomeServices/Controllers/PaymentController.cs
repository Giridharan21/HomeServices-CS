using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Models;
namespace HomeServices.Controllers
{
    public class PaymentController : Controller
    {
        //Payment Giri
        [HttpGet]
        public ActionResult Pay(PaymentModel PayObj)
        {
            return View(PayObj);
        }
        [HttpPost]
        public ActionResult Pay(string Password)
        {

            return View();
        }
    }
}