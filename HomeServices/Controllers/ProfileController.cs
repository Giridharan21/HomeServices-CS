using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
namespace HomeServices.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            List<DataAccessLayer.Models.Profile> UserDataList= Data.Profile(); 
            return View();
        }
    }
}