using bookMaintain.Common;
using bookMaintain.Model.BackEnd.Arg.Food;
using bookMaintain.Model.Models;
using bookMaintain.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public ViewResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception parameterEx)
            {
                Logger.Write(Logger.LogCategoryEnum.Error, parameterEx.ToString());
                return View("Error");
            }
        }

        [HttpGet()]
        public JsonResult GetFood()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return this.Json(new { data = foodService.GetFoodMysql() });
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

        [HttpGet()]
        public JsonResult GetFoodPageMysql(DataTablesRequest FoodPage/*SearchArg bookMaintainSearchArg*/)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return new JsonResult(foodService.GetFoodPageMysql(FoodPage));
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

        [HttpGet()]
        public JsonResult GetFoodOrderQuantity()
        {
            try
            {

                return new JsonResult(foodService.GetFoodOrderQuantity());
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

        [HttpPost()]
        public JsonResult BuyFood(string buyJson)
        //public JsonResult BuyFood([FromBody]BuyFoodModel buyJson)
        //public JsonResult BuyFood(BuyFoodModel buyJson)
        {
            try
            {
                /*using (var reader = new StreamReader(Request.Body))
                {
                    var body = reader.ReadToEnd();
                    Console.WriteLine(body);
                    // Do something
                }*/
                //Console.WriteLine(Request.Form["body"]);
                //FoodModel ccc = buyJson.foodModel.First();
                //Console.WriteLine(ccc.name);
                dynamic updateData = JsonConvert.DeserializeObject(buyJson);
                //Console.WriteLine(updateData.buyJson);
                //Console.WriteLine(updateData[0].name);
                int result = foodService.BuyFood(updateData);
                return this.Json(new { datas = result });
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

        public class BuyFoodModel
        {
            public List<FoodModel> foodModel { get; set; }
        }

        public class FoodModel
        {
            public int id { get; set; }
            public string name { get; set; }
            public string number { get; set; }
        }
    }
}
