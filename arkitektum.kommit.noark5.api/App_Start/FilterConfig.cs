using System.Web;
using System.Web.Mvc;

namespace arkitektum.kommit.noark5.api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
