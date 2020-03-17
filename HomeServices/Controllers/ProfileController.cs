using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Models;
using DataAccessLayer;
namespace HomeServices.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Master()
        {
            var User = (UserInfoModel)Session["UserData"];
            List<DataAccessLayer.Models.Profile> UserDataList= Data.Profile(User.Id); 
            return View(UserDataList);
        }
        public ActionResult EditView()
        {
           
            return View();
        }
    }
}