using ExternalAccountWebAuthentication.Authentication;
using System;
using System.Web.Http;  

namespace AccountExternalWeb.ApiControllers
{
    [RoutePrefix("TestApi")]
    public class TestApiController : BaseApiController
    {
        [Route("Get")]
        public IHttpActionResult Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        [Route("RoleTest")]
        [ApiAuthorizationFilterAttribute(false, new string[] { "ExternalAccountAdministrator" })]
        public IHttpActionResult RoleTest()
        {
            try
            {
                return Ok(CredentialId);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
