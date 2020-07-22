using System.Web;
using System.Web.Mvc;

namespace CubeSYSVN_Intern_anhthai
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
