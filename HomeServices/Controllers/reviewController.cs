using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Models;
using DataAccessLayer;
namespace HomeServices.Controllers
{
    public class reviewController : Controller
    {
        // GET: review
        public ActionResult Index(int OrderId)
        {
            Session["orderid"] = OrderId;
            return View();
        }
        [HttpPost]
        public ActionResult Index(reviewmodel model)
        {
            int orderid = int.Parse(Session["orderid"].ToString());
            if(ModelState.IsValid)
            {
                Data.AddReview(model,orderid);
                return RedirectToAction("MyOrders","CustomerDashboard");
            }
            return View();
        }
    }
}