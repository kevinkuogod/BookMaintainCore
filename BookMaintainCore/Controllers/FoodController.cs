using bookMaintain.Common;
using bookMaintain.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookMaintainCore.Controllers
{
    public class FoodController : Controller
    {
        private IFoodService foodService;
        public FoodController()
        {
            this.foodService = new FoodService();
        }
        
        [HttpGet()]
        public JsonResult GetFood(/*SearchArg bookMaintainSearchArg*/)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //return this.Json(new { datas = foodService.GetFood(/*bookMaintainSearchArg*/) });
                    return this.Json(new { datas = foodService.GetFoodMysql(/*bookMaintainSearchArg*/) });
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
                    new { type = "main", message = parameterEx.ToString()/*"主表取得過程錯誤"*/, code = (int)ErrorCode.ErrorCodeField.tableMainError }
                    , HttpStatusCode.InternalServerError);
            }
        }
    }
}
