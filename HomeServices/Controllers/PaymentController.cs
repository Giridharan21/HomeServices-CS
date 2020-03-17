using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Models;
using DataAccessLayer;
namespace HomeServices.Controllers
{
    public class PaymentController : Controller
    {
        //Payment Giri
        [HttpGet]
        public ActionResult Pay(int OrderId)
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Pay(PaymentModel PayObj)
        {

            return View();
        }
    }
}