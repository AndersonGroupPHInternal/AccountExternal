using ExternalAccountWebAuthentication.Authentication;
using System.Web.Mvc;

namespace AccountExternalWeb.Controllers
{
    [MvcAuthorizationFilterAttribute(false, "Credential", "Login", new string[] { })]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}