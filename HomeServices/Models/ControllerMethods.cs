using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.Models;

namespace HomeServices.Models
{
    public class ControllerMethods:Controller
    {
        public int SetLogInInfo(string type) {
            if (type == "Customer") {
                ViewBag.Login = "Customer";
            }
            return 1;
        }
    }
}