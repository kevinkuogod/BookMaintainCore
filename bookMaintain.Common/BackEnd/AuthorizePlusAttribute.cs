
using Microsoft.AspNetCore.Authorization;


namespace bookMaintain.Common
{
    public class AuthorizePlusAttribute : AuthorizeAttribute
    {
        /*public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //base.OnAuthorization(filterContext);
            var auth = Convert.ToBoolean(filterContext.HttpContext.Session["auth"]);
            if (auth)
            {
                //驗證成功
            }
            else
            {
                //想換成自己的
                base.HandleUnauthorizedRequest(filterContext);
            }
        }*/
    }
}