using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Models;

namespace HomeServices.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult ViewProfile()
        {
            var Details = (UserInfoModel)Session["UserData"];
            
            return View(Details);
        }
        
        public ActionResult CustomerEditProfile(int id)
        {
            var CustomerDetails=Data.CustomerData(id);
            return View(CustomerDetails);
        }
        [HttpPost]
        public ActionResult CustomerEditProfile(CustomerProfile obj)
        {
            var Details = (UserInfoModel)Session["UserData"];
            Data.EditProfile(obj, Details.Id);
            return RedirectToAction("ViewProfile");
        }
        public ActionResult SPEditProfile(int id)
        {
            var SPDetails = Data.SPData(id);
            return View(SPDetails);
            
        }
        [HttpPost]
        public ActionResult SPEditProfile(ServiceProviderProfile obj)
        {
            var Details = (UserInfoModel)Session["UserData"];
            Data.EditProfile(obj, Details.Id);
            return RedirectToAction("ViewProfile");
        }

    }
}