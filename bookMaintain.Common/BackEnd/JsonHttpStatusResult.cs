//using System.Web.Mvc;

using Microsoft.AspNetCore.Mvc;

namespace bookMaintain.Common
{
    public class JsonHttpStatusResult : JsonResult
    {
        private readonly System.Net.HttpStatusCode _httpStatus;
        private object Data;

        //自動繼承了父類的屬性
        public JsonHttpStatusResult(object data, System.Net.HttpStatusCode httpStatus) : base(data, httpStatus)
        {
            Data = data;
            _httpStatus = httpStatus;
        }

        /*
        public override void ExecuteResult(ControllerContext context)
        {
            context.RequestContext.HttpContext.Response.StatusCode = (int)_httpStatus;
            base.ExecuteResult(context);
        }
        */
    }
}