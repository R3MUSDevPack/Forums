using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace MVCForum.Website.CustomAttributes
{
    public class WarAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return actionContext.IsLocal();
        }
    }

    public static class HttpActionContextExtensions
    {
        public static bool IsLocal(this HttpActionContext me)
        {
            return (me.Request.RequestUri.DnsSafeHost == "localhost");
        }
    }
}