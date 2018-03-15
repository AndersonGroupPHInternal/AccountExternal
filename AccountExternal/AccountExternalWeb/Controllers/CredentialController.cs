using AccountExternalFunction;
using AccountExternalModel;
using ExternalAccountWebAuthentication.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AccountExternalWeb.Controllers
{
    public class CredentialController : BaseController
    {
        private IFCredential _iFCredential;
        private IFCredentialRole _iFCredentialRole;
        public CredentialController(IFCredential iFCredential, IFCredentialRole iFCredentialRole)
        {
            _iFCredential = iFCredential;
            _iFCredentialRole = iFCredentialRole;
        }

        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Credential());
        }

        [HttpPost]
        public ActionResult Create(Credential credential)
        {
            var createdCredential = _iFCredential.Create(CredentialId, credential);
            _iFCredentialRole.Create(CredentialId, createdCredential.CredentialId, credential.CredentialRoles);
            return RedirectToAction("Index");
        }
        #endregion

        #region Read
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Login")]
        [MvcAuthorizationFilter(true)]
        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception exception)
            {
                return Json(exception.ToString());
            }
        }

        [Route("Login")]
        [MvcAuthorizationFilter(true)]
        [HttpPost]
        public ActionResult Login(Credential credential)
        {
            try
            {
                credential = _iFCredential.Login(credential);
                bool isLogin = credential.CredentialId > 0;
                if (isLogin)
                {
                    string encryptedUsername = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(credential.Username)));
                    string encryptedId = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(credential.CredentialId.ToString())));
                    HttpCookie credentialCookies = new HttpCookie("Credential");
                    credentialCookies["Username"] = encryptedUsername;
                    credentialCookies["CredentialId"] = encryptedId;
                    credentialCookies.Expires = DateTime.Now.AddHours(24);
                    Response.Cookies.Add(credentialCookies);
                    return Redirect("~/Home");
                }
                return View();
            }
            catch (Exception exception)
            {
                return Json("Error on logging in");
            }
        }

        [HttpPost]
        public JsonResult Read()
        {
            return Json(_iFCredential.Read());
        }
        #endregion

        #region Update
        [MvcAuthorizationFilterAttribute(false, "Credential", "Login", new string[] { })]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [MvcAuthorizationFilterAttribute(false, "Credential", "Login", new string[] {  })]
        [HttpPost]
        public ActionResult ChangePassword(Credential credential)
        {
            if (ModelState.IsValid)
            {
                var createdCredential = _iFCredential.ChangePassword(CredentialId, credential);
            }
            else if (!ModelState.IsValid)
            {
                return View(credential);
            }
            return Redirect("~/Home");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            return View(_iFCredential.Read(id));
        }

        [HttpPost]
        public ActionResult Update(Credential credential)
        {
            var createdCredential = _iFCredential.Update(CredentialId, credential);
            _iFCredentialRole.Create(CredentialId, createdCredential.CredentialId, credential.CredentialRoles);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Signout()
        {
            HttpCookie credentialCookies = new HttpCookie("Credential");
            credentialCookies.Expires = DateTime.Now.AddHours(-1);
            Response.Cookies.Add(credentialCookies);
            return Redirect("~/Home");
        }
        #endregion

        #region Delete
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            _iFCredential.Delete(id);
            return Json(string.Empty);
        }
        #endregion

        #region Other Function

        #endregion
    }
}