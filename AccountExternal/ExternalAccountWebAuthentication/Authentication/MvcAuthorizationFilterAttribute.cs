using AccountExternalData;
using AccountExternalFunction;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace ExternalAccountWebAuthentication.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MvcAuthorizationFilterAttribute : AuthorizeAttribute
    {
        private bool AllowAnonymous;
        private string RedirectController;
        private string RedirectMethod;
        private string[] AllowedRoles;

        private IDCredential _iDCredential;
        private IDRole _iDRole;
        private IFRole _iFRole;
        private IFCredential _iFCredential;

        public MvcAuthorizationFilterAttribute(bool allowAnonymous)
        {
            AllowAnonymous = allowAnonymous;
            RedirectController = string.Empty;
            RedirectMethod = string.Empty;
            AllowedRoles = new string[0];
        }

        public MvcAuthorizationFilterAttribute(bool allowAnonymous, string[] allowedRoles)
        {
            AllowAnonymous = allowAnonymous;
            AllowedRoles = allowedRoles;
            RedirectController = string.Empty;
            RedirectMethod = string.Empty;
        }

        public MvcAuthorizationFilterAttribute(bool allowAnonymous, string redirectController, string redirectMethod)
        {
            AllowAnonymous = allowAnonymous;
            RedirectController = redirectController;
            RedirectMethod = redirectMethod;
            AllowedRoles = new string[0];
        }

        public MvcAuthorizationFilterAttribute(bool allowAnonymous, string redirectController, string redirectMethod, string[] allowedRoles)
        {
            AllowAnonymous = allowAnonymous;
            AllowedRoles = allowedRoles;
            RedirectController = redirectController;
            RedirectMethod = redirectMethod;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool authorized = false;
            if (AllowAnonymous || (Cookies.IsLoggedIn && AllowedRoles.Length == 0))
            {
                authorized = true;
            }
            else if (Cookies.IsLoggedIn)
            {
                _iDCredential = new DCredential();
                _iDRole = new DRole();
                _iFCredential = new FCredential(_iDCredential);
                _iFRole = new FRole(_iDRole);

                authorized = _iFRole.HasRole(Cookies.CredentialId, AllowedRoles);
            }
            
            if (!authorized && !string.IsNullOrEmpty(RedirectController) && !string.IsNullOrEmpty(RedirectMethod))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = RedirectController, action = RedirectMethod }));
            }
            else if (!authorized)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}
