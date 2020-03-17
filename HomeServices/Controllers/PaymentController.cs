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
            Session["Payment"] = "";
            var NewPayment = Data.GetOrder(OrderId);
                     
            return View(NewPayment);
        }
        [HttpPost]
        public ActionResult Pay(PaymentModel PayObj)
        {
            var result = Data.Authenticate(PayObj);
            if(result == 0)
            {
                Session["Payment"] = "alert('Invalid Password')";
                return RedirectToAction("MyOrders", "CustomerDashboard");
            }
            Session["Payment"] = "alert('Payment Successfull')";
            Data.MakePayment(PayObj);
            return RedirectToAction("MyOrders","CustomerDashboard");
        }
    }
}