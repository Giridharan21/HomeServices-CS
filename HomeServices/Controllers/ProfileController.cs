﻿using System;
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

    }
}