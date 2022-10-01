using System;
using System.Collections.Generic;
using System.Net;
using bookMaintain.Service;
using Newtonsoft.Json;
using bookMaintain.Common;
using bookMaintain.Model.BackEnd.Arg.Select;
using bookMaintain.Model.BackEnd.Arg.Input;
using bookMaintain.Model.BackEnd.Arg.BookMaintain;
using Microsoft.AspNetCore.Mvc;

namespace BookMaintain.Controllers
{
    public class RegisterController : Controller
    {
        //private IBookMaintainService bookMaintainService { get; set; }
        private IRegisterService registerService;
        public RegisterController()
        {
            this.registerService = new RegisterService();
        }

        /// <summary>
        /// 註冊系統
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View("Index");
            }
            catch (Exception parameterEx)
            {
                Logger.Write(Logger.LogCategoryEnum.Error, parameterEx.ToString());
                return View("Error");
            }
        }

        /// <summary>
        /// 註冊(實作)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Insert(string registerJson)
        {
            try
            {
                dynamic registerData = JsonConvert.DeserializeObject(registerJson);
;
                Register register = new Register()
                {
                    FIRST_NAME = registerData.FirstName.Value,
                    LAST_NAME = registerData.LastName.Value,
                    EMAIL = registerData.Email.Value,
                    TELEPHONE = registerData.Telephone.Value,
                    FAX = registerData.FAX.Value,
                    PASSWORD = registerData.Password.Value
                };

                if (ModelState.IsValid)
                {
                    int registerdata = registerService.Insert(register);
                    if (registerdata > 0)
                    {
                        return this.Json(new { type = "insert", message = "創建成功" });
                    }
                    else
                    {
                        return this.Json(new { type = "insert", message = "創建失敗" });
                    }
                }
                else
                {
                    return this.Json(new { type = "insert", message = "創建失敗" });
                }

                //[ID][bigint] NOT NULL,                 /*顧客ID*/
                //[FIRSTNAME] [nvarchar] (50) NOT NULL,	/*顧客'姓'後名稱*/
                //[LASTNAME] [nvarchar] (50) NOT NULL,	/*顧客'姓'名稱*/
                //[ROLE] [int] NULL,			/*角色*/
                //[EMAIL][nvarchar] (250) NOT NULL,       /*信箱*/
                //[EMAIL_VERIFIED_AT] [datetime] NULL,	/*信箱驗證時間*/
                //[TELEPHONE][nvarchar] (50) NULL,	/*電話名稱*/
                //[FAX][nvarchar] (50) NULL,		/*傳真*/
                //[PASSWORD][nvarchar] (255) NOT NULL,	/*密碼*/
                //[REMEMBER_TOKEN] [nvarchar] (100) NOT NULL,	/*備註*/
                //[CREATE_DATE] [datetime] NULL,	/*創建時間*/
                //[CREATE_USER][nvarchar] (50) NULL, /*創建者*/
                //[MODIFY_DATE][datetime] NULL,	/*更改時間*/
                //[MODIFY_USER][nvarchar] (50) NULL, /*更改使用者*/

            }
            catch (Exception parameterEx)
            {
                Logger.Write(Logger.LogCategoryEnum.Error, parameterEx.ToString());
                return new JsonHttpStatusResult(
                           new { type = "register", message = "註冊過程錯誤", code = (int)ErrorCode.ErrorCodeField.insertBookError }
                            , HttpStatusCode.InternalServerError);
            }
        }
    }
}