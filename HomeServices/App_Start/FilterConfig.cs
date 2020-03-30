using System.Web;
using System.Web.Mvc;
using HomeServices.Filter_Class;
namespace HomeServices
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new CustomExceptions());
        }
    }
}
