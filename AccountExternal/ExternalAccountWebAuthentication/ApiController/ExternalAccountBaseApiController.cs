using ExternalAccountWebAuthentication.Authentication;
using System.Web.Http.Cors;

namespace ExternalAccountWebAuthentication.ApiController
{
    [EnableCors("*", "*", "*")]
    public class ExternalAccountBaseApiController : System.Web.Http.ApiController
    {
        protected int CredentialId
        {
            get
            {
                return Claims.CredentialId;
            }
        }
        
        protected string Username
        {
            get
            {
                return Claims.Username;
            }
        }
    }
}
