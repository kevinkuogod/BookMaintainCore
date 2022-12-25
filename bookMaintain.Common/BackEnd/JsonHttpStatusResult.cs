//using System.Web.Mvc;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.VisualBasic;
using System.Resources;

namespace bookMaintain.Common
{

    public class JsonHttpStatusResult : JsonResult
    {
        //自動繼承了父類的屬性
        private readonly System.Net.HttpStatusCode _httpStatus;

        public JsonHttpStatusResult(object data, System.Net.HttpStatusCode httpStatus) : base(data)
        {
             Value = data;
            _httpStatus = httpStatus;
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)_httpStatus;
            return base.ExecuteResultAsync(context);
        }
    }
}