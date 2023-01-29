using System.Net;
using bookMaintain.Service;
using Newtonsoft.Json;
using bookMaintain.Common;
using bookMaintain.Model.BackEnd.Arg.Select;
using bookMaintain.Model.BackEnd.Arg.Input;
using bookMaintain.Model.BackEnd.Arg.BookMaintain;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BookMaintain.Controllers
{
    //[RoutePrefix("api/value")] //需要匯入套件
    public class BookMaintainController : Controller
    {
        //private IBookMaintainService bookMaintainService { get; set; }
        private IBookMaintainService bookMaintainService;
        private IBookLendRecordService bookLendRecordService;
        private ISelectService selectService;
        private IRegisterService registerService;
        public BookMaintainController()
        {
            this.bookMaintainService   = new BookMaintainService();
            this.selectService         = new SelectService();
            this.bookLendRecordService = new BookLendRecordService();
            this.registerService       = new RegisterService();
        }

        /// <summary>
        /// 圖書維護主查詢(視窗)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        //[Authorize(Roles = "Administrator")]
        //[Authorize]
        //[Authorize(roles = "Admin")] 使用 roles 屬性的話，就會自動讀取 ClaimTypes.Role 的內容。
        //[Authorize(.AspNetCore.Cookies.)]
        //[Authorize(Policy = ".AspNetCore.Cookies")]
        //[Route("api/value/{id:int}")]
        //[AuthorizePlusAttribute]
        public ActionResult Index()
        {
            //HttpContext.Current.Request.QueryString("456");
            //var userInfo = new UserInfo(User.Claims.ToList()); //只有用了Authorize才能使用
            //userInfo.Name
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

        /// <summary>
        /// 圖書維護查詢(表單單欄位下拉元件分類資料)
        /// </summary>
        /// <returns></returns>
        public List<SelectListValue> getFormSelectDataType(string type)
        {
            List<SelectListValue> obj = new List<SelectListValue>();
            switch (type)
            {
                case "bookClassIdText":
                    obj = selectService.GetBookClass();
                    break;
                case "codeIdText":
                    obj = selectService.GetBookCodeStatus("BOOK_STATUS");
                    break;
                case "userIdTextOri":
                    obj = selectService.GetBookKeeper("EName");
                    break;
                case "userIdTextCE":
                    obj = selectService.GetBookKeeper("CEName");
                    break;
                default:
                    break;
            }
            return obj;
        }

        /// <summary>
        /// 圖書維護查詢(表單單欄位輸入元件分類資料)
        /// </summary>
        /// <returns></returns>
        public List<InputListValue> getFormInputDataType(string type)
        {
            List<InputListValue> obj = new List<InputListValue>();
            switch (type)
            {
                case "bookName":
                    //obj = bookMaintainService.GetInputTableBootstarp("BOOK_DATA", "Original");
                    break;
                default:
                    break;
            }
            return obj;
        }

        /// <summary>
        /// 圖書維護查詢(最新的輸入書本數)
        /// </summary>
        /// <returns></returns>
        public JsonResult getBookNumber(string type)
        {
            try
            {
                int number = 0;
                switch (type)
                {
                    case "newBook":
                        //number = bookMaintainService.GetNetBookNumber();
                        break;
                    default:
                        break;
                }
                return this.Json(new { data = number, message = "success" }); ;
            }
            catch (Exception parameterEx)
            {
                Logger.Write(Logger.LogCategoryEnum.Error, parameterEx.ToString());
                return new JsonHttpStatusResult(
                    new { type = "formSelectData", message = "下拉式選單錯誤", code = (int)ErrorCode.ErrorCodeField.formSelectError }
                    , HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 圖書維護查詢(表單下拉式欄位資料)，需要改model
        /// ValidateAntiForgeryToken:CORS的TOKEN
        /// [Bind(Include = "ID,Title,ReleaseDate,Genre,Price")],[Bind(Include = "type")];放在型態前面
        /// </summary>
        /// <returns></returns>
        //[HttpGet()]
        [HttpPost()]
        //ValidateHeaderAntiForgeryTokenAttribute]
        //[FromUri] //SelectJson json
        public JsonResult getFormSelectData(SelectJson json)
        {
            try
            {
                //return this.json(this.getformdatatype(type), jsonrequestbehavior.allowget);
                if (ModelState.IsValid)
                {
                    return this.Json(new { data = this.getFormSelectDataType(json.type), message = "success" });
                }
                else
                {
                    Logger.Write(Logger.LogCategoryEnum.Error, "模型驗證錯誤");
                    return new JsonHttpStatusResult(
                    new { type = "formSelectData", message = "模型驗證錯誤", code = (int)ErrorCode.ErrorCodeField.formSelectError }
                    , HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception parameterEx)
            {
                Logger.Write(Logger.LogCategoryEnum.Error, parameterEx.ToString());
                return new JsonHttpStatusResult(
                    new { type = "formSelectData", message = "下拉式選單錯誤", code = (int)ErrorCode.ErrorCodeField.formSelectError }
                    , HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 圖書維護查詢(表單輸入欄位資料)
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        //[ValidateHeaderAntiForgeryTokenAttribute]
        public JsonResult getFormInputData(InputJson json)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return this.Json(new { data = this.getFormInputDataType(json.type), message = "success" });
                }
                else
                {
                    Logger.Write(Logger.LogCategoryEnum.Error, "模型驗證錯誤");
                    return new JsonHttpStatusResult(
                    new { type = "formInput", message = "模型驗證錯誤", code = (int)ErrorCode.ErrorCodeField.formInputError }
                    , HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception parameterEx)
            {
                Logger.Write(Logger.LogCategoryEnum.Error, parameterEx.ToString());
                return new JsonHttpStatusResult(
                    new { type = "formInput", message = parameterEx.ToString(), code = (int)ErrorCode.ErrorCodeField.formInputError }
                    , HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 圖書維護查詢(查詢表資料)
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        public JsonResult getTableMainData(SearchArg bookMaintainSearchArg)
        {
            try { 
                if (ModelState.IsValid)
                {
                    return this.Json(new { data = bookMaintainService.GetTable(bookMaintainSearchArg) });
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

        /// <summary>
        /// 輸入書本頁面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Insert()
        {
            try
            {
                return View("Insert");
            }
            catch (Exception parameterEx)
            {
                Logger.Write(Logger.LogCategoryEnum.Error, parameterEx.ToString());
                return View("Error");
            }
        }

        /// <summary>
        /// 新增圖書維護(新增)
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        //[ValidateAntiForgeryToken]
        public JsonResult Insert(string insertJson)
        {
            //ModelState.IsValid
            if (string.IsNullOrEmpty(insertJson))
            {
                return new JsonHttpStatusResult(
                           new { type = "insert", message = "新增值為空", code = (int)ErrorCode.ErrorCodeField.insertBookError }
                            , HttpStatusCode.InternalServerError);
            }
            else
            {
                try
                {
                    dynamic insertData = JsonConvert.DeserializeObject(insertJson);
                    //Model會上參數驗證

                    InsertArg bookMaintainInsert = new InsertArg()
                    {
                        BOOK_NAME = insertData.BOOK_NAME.Value,
                        BOOK_AUTHOR = insertData.BOOK_AUTHOR.Value,
                        BOOK_PUBLISHER = insertData.BOOK_PUBLISHER.Value,
                        BOOK_NOTE = System.Uri.UnescapeDataString(insertData.BOOK_NOTE.Value),
                        BOOK_BOUGHT_DATE = Convert.ToDateTime(insertData.BOOK_BOUGHT_DATE.Value).ToString("yyyy-MM-dd HH:mm:ss:fff"),
                        BOOK_CLASS_ID = insertData.BOOK_CLASS_ID.Value
                    };
                    //int errorNumber = bookMaintainService.InsertBookMaintain(bookMaintainInsert);
                    int errorNumber = 0;
                    if (errorNumber > 0)
                    {
                        return this.Json(new { type = "insert", message = "新增成功" });
                    }
                    else
                    {
                        return new JsonHttpStatusResult(
                           new { type = "insert", message = "新增過程錯誤", code = errorNumber }
                            , HttpStatusCode.InternalServerError);
                    }
                }
                catch (Exception parameterEx)
                {
                    Logger.Write(Logger.LogCategoryEnum.Error, parameterEx.ToString());
                    return new JsonHttpStatusResult(
                           new { type = "insert", message = "新增過程錯誤", code = (int)ErrorCode.ErrorCodeField.insertBookError }
                            , HttpStatusCode.InternalServerError);
                }
            }
        }

        /// <summary>
        /// 圖書維護主查詢(視窗)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Update()
        {
            try
            {
                return View("Update");
            }
            catch (Exception parameterEx)
            {
                Logger.Write(Logger.LogCategoryEnum.Error, parameterEx.ToString());
                return View("Error");
            }
        }

        /// <summary>
        /// 圖書維護畫面(修改) update需要測試四種值的狀態跟值能不能為空，postman測試，回傳值要寫commend
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        //[ValidateAntiForgeryToken]
        public JsonResult UpdateBookMaintain(string updateJson)
        {
            //ModelState.IsValid
            if (string.IsNullOrEmpty(updateJson))
            {
                return new JsonHttpStatusResult(
                          new { type = "update", message = "更新值為空", code = (int)ErrorCode.ErrorCodeField.updateBookError }
                           , HttpStatusCode.InternalServerError);
            }
            else
            {
                try
                {
                    dynamic updateData = JsonConvert.DeserializeObject(updateJson);
                    //型態轉換讓model可以共用，打錯字抓不出來
                    int bookId;
                    bool result = Int32.TryParse(Convert.ToString(updateData.BOOK_ID.Value), out bookId);
                    if ((!result) && (bookId < 0))
                    {
                        return this.Json(new { type = "update", message = "bookId轉型失敗", code = -5 });
                    }

                    UpdatePost bookMaintainUpdatePost = new UpdatePost()
                    {
                        BOOK_AUTHOR = updateData.BOOK_AUTHOR.Value,
                        BOOK_BOUGHT_DATE = updateData.BOOK_BOUGHT_DATE.Value,
                        BOOK_CLASS_ID = updateData.BOOK_CLASS_ID.Value,
                        BOOK_ID = bookId,
                        BOOK_NAME = updateData.BOOK_NAME.Value,
                        BOOK_NOTE = System.Uri.UnescapeDataString(updateData.BOOK_NOTE.Value),
                        BOOK_PUBLISHER = updateData.BOOK_PUBLISHER.Value,
                        CODE_ID = updateData.BOOK_CODE_ID.Value,
                        USER_ID = updateData.BOOK_USER_ID.Value
                    };
                    //int errorNumber = bookMaintainService.UpdateBookMaintain(bookMaintainUpdatePost);
                    int errorNumber = 1;
                    if (errorNumber == 0)
                    {
                        return this.Json(new { type = "update", message = "更新成功" });
                    }
                    else
                    {
                        return this.Json(new { type = "update", message = "更新過程錯誤", code = errorNumber });
                    }
                }
                catch (Exception parameterEx)
                {
                    Logger.Write(Logger.LogCategoryEnum.Error, parameterEx.ToString());
                    return new JsonHttpStatusResult(
                          new { type = "update", message = "更新書本資料錯誤", code = (int)ErrorCode.ErrorCodeField.updateBookError }
                           , HttpStatusCode.InternalServerError);
                }
            }
        }

        /// <summary>
        /// 刪除圖書維護畫面(刪除) string?
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public JsonResult DeleteBookMaintain(string deleteJson)
        {
            //ModelState.IsValid
            try
            {
                dynamic deleteParseJson = JsonConvert.DeserializeObject(deleteJson);
                int errorNumber = 0;
                switch (Convert.ToString(deleteParseJson.CODE_ID.Value))
                {
                    case "B":
                    case "C":
                        errorNumber = -1;
                        break;
                }


                int bookId;
                bool result = Int32.TryParse(Convert.ToString(deleteParseJson.BOOK_ID.Value), out bookId);
                if ((!result) && (bookId < 0))
                {
                    errorNumber = -5;
                }

                if (errorNumber == 0)
                {
                    //errorNumber = bookMaintainService.DeleteBookMaintain(bookId);
                    errorNumber = 1;
                    if (errorNumber == 0)
                    {
                        return this.Json(new { type = "destory", message = "刪除成功", code = errorNumber });
                    }
                    else
                    {
                        return this.Json(new { type = "destory", message = "刪除過程錯誤", code = errorNumber });
                    }
                }
                else
                {
                    switch (errorNumber)
                    {
                        case -1:
                            return new JsonHttpStatusResult(
                                new { type = "destory", message = "借閱中不可刪除", code = (int)ErrorCode.ErrorCodeField.deleteBookError }
                                , HttpStatusCode.InternalServerError);
                        case -5:
                            return new JsonHttpStatusResult(
                                new { type = "destory", message = "bookId轉型失敗", code = (int)ErrorCode.ErrorCodeField.deleteBookError }
                                , HttpStatusCode.InternalServerError);
                        default:
                            return new JsonHttpStatusResult(
                                new { type = "destory", message = "刪除過程錯誤", code = errorNumber }
                                , HttpStatusCode.InternalServerError);
                    }
                }
            }
            catch (Exception parameterEx)
            {
                Logger.Write(Logger.LogCategoryEnum.Error, parameterEx.ToString());
                return new JsonHttpStatusResult(
                                new { type = "destory", message = "刪除過程錯誤", code = (int)ErrorCode.ErrorCodeField.deleteBookError }
                                , HttpStatusCode.InternalServerError);
            }
        }
    }
}