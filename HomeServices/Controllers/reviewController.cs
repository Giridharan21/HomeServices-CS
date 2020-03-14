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
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(reviewmodel model)
        {
            if(ModelState.IsValid)
            {
                Data.AddReview(model);
                return View();
            }
            return View();
        }
    }
}