using bookMaintain.Common;
using bookMaintain.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookMaintainCore.Controllers
{
    public class ArticleController : Controller
    {
        private IArticleService articleService;
        public ArticleController()
        {
            this.articleService = new ArticleService();
        }

        [HttpPost]
        public async Task<JsonResult> EditorFile(IFormFile file)
        {
            if (await articleService.EditorFile(file) == 1)
            {
                return this.Json(new { type = "insert", message = "上傳成功" });
            }
            else
            {
                return this.Json(new { type = "insert", message = "上傳失敗" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddComment(string ArticleContent)
        {
            if (await articleService.AddComment(ArticleContent) > 0)
            {
                return this.Json(new { type = "insert", message = "上傳成功" });
            }
            else
            {
                return this.Json(new { type = "insert", message = "上傳失敗" });
            }
        }
        
        [HttpGet()]
        public JsonResult getComment(/*SearchArg bookMaintainSearchArg*/)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return this.Json(new { datas = articleService.GetComment(/*bookMaintainSearchArg*/) });
                }
                else
                {
                    Logger.Write(Logger.LogCategoryEnum.Error, "模型驗證錯誤");
                    return new JsonHttpStatusResult(
                    new { type = "main", message = "模型驗證錯誤", code = (int)ErrorCode.ErrorCodeField.tableMainError }
                    , HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception parameterEx)
            {
                Console.WriteLine(parameterEx);
                Logger.Write(Logger.LogCategoryEnum.Error, parameterEx.ToString());
                return new JsonHttpStatusResult(
                    new { type = "main", message = "主表取得過程錯誤", code = (int)ErrorCode.ErrorCodeField.tableMainError }
                    , HttpStatusCode.InternalServerError);
            }
        }
    }
}
