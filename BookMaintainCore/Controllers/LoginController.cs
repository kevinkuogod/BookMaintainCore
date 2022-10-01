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
    public class LoginController : Controller
    {
        private ILoginService loginService;
        public LoginController()
        {
            this.loginService = new LoginService();
        }

        /// <summary>
        /// 登入系統
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
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

        /// <summary>
        /// 登入系統
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Logout()
        {
            try
            {
                //Session["auth"] = false;
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
        public JsonResult Insert(string loginJson)
        {
            try
            {
                dynamic loginData = JsonConvert.DeserializeObject(loginJson);

                Login login = new Login()
                {
                    EMAIL = loginData.Email.Value,
                    PASSWORD = loginData.Password.Value
                };

                if (ModelState.IsValid)
                {
                    int determineLogin = loginService.Insert(login);
                    if (determineLogin == 1)
                    {
                        //Session["auth"] = true;
                        return this.Json(new { type = "insert", message = "登入成功" });
                 
                    }
                    else
                    {
                        return this.Json(new { type = "insert", message = "登入失敗" });
                    }
                }
                else
                {
                    return this.Json(new { type = "insert", message = "登入失敗" });
                }

            }
            catch (Exception parameterEx)
            {
                Logger.Write(Logger.LogCategoryEnum.Error, parameterEx.ToString());
                return new JsonHttpStatusResult(
                           new { type = "login", message = "登入過程錯誤", code = (int)ErrorCode.ErrorCodeField.insertBookError }
                            , HttpStatusCode.InternalServerError);
            }
        }
    }
}