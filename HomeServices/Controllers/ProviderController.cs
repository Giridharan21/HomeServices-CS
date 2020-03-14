using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Models;
using DataAccessLayer;

namespace HomeServices.Controllers
{
    public class ProviderController : Controller
    {
        // GET: Provider
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult ReceivedOrder()
        {
            var User = (UserInfoModel)Session["UserData"];
            var res = Data.ServiceProvider(User.Id);
            return View(res);
        }

        [HttpPost]
        public ActionResult PlaceOrder(int OrderId, string Status)
        {
            Data.Accept(OrderId, Status);
            return Redirect("~/Provider/ReceivedOrder/");
        }

        public ActionResult Previousorder()
        {
            var User = (UserInfoModel)Session["UserData"];
            var res = Data.ServiceProvider(User.Id);
            return View(res);

        }
    }
}