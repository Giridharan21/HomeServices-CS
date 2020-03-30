using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace HomeServices.Filter_Class
{
    public class CustomExceptions : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            if (filterContext.Exception is NullReferenceException)
            {
                filterContext.Result = new ViewResult() { ViewName = "InvalidAccess" };
            }
            else if (filterContext.Exception is ArgumentException)
            {
                filterContext.Result = new ViewResult() { ViewName = "InvalidArgument" };
            }
            else
            {
                filterContext.Result = new ViewResult() { ViewName = "Unknown" };
            }
        }
    }
}