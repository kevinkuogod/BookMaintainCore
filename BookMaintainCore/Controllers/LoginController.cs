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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using static System.Net.Mime.MediaTypeNames;
using bookMaintain.Dao.BackEnd.Ado;

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
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
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
        //,string ReturnUrl
        /*
         * if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl) )
            {
                return Redirect(ReturnUrl);//導到原始要求網址
            }
        HttpContext.User.Claims.FirstOrDefault(m=>m.Type=="Account").Value;
         * */
        //public JsonResult Insert(string loginJson)
        /*dynamic loginData = JsonConvert.DeserializeObject(loginJson);

Login login = new Login()
{
    EMAIL = loginData.Email.Value,
    PASSWORD = loginData.Password.Value
};*/
        public JsonResult Insert(Login loginJson)
        {
            try
            {
                Login login = new Login()
                {
                    EMAIL = loginJson.EMAIL,
                    PASSWORD = loginJson.PASSWORD
                };

                if (ModelState.IsValid)
                {
                    int determineLogin = loginService.Insert(login);
                    if (determineLogin == 1)
                    {
                        //Session["auth"] = true;
                        var claims = new List<Claim>() {
                            new Claim(ClaimTypes.NameIdentifier,"test"),
                            new Claim(ClaimTypes.Name,login.EMAIL.ToString()),
                            new Claim(ClaimTypes.Role, "Administrator")
                        };
                        //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsIdentity = new ClaimsIdentity(
                                           claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            // 在此設定Cookies相關細節，如過期時間...等
                            //AllowRefresh = <bool>,
                            // Refreshing the authentication session should be allowed.

                            //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10), 
                            // The time at which the authentication ticket expires. A 
                            // value set here overrides the ExpireTimeSpan option of 
                            // CookieAuthenticationOptions set with AddCookie.
                            
                            // 持久保存
                            IsPersistent = true,
                            // Whether the authentication session is persisted across 
                            // multiple requests. When used with cookies, controls
                            // whether the cookie's lifetime is absolute (matching the
                            // lifetime of the authentication ticket) or session-based.

                            //IssuedUtc = <DateTimeOffset>,
                            // The time at which the authentication ticket was issued.

                            //RedirectUri = <string>
                            // The full path or absolute URI to be used as an http redirect response value.

                            ExpiresUtc = DateTime.UtcNow.AddMinutes(30),//過期時間：30分鐘
                        };

                        //HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                        new ClaimsPrincipal(claimsIdentity),
                                                        authProperties);
                        return this.Json(new { type = "login", message = "登入成功" });
                 
                    }
                    else
                    {
                        return this.Json(new { type = "login", message = "登入失敗" });
                    }
                }
                else
                {
                    return this.Json(new { type = "login", message = "登入失敗" });
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

        /// <summary>
        /// 忘記密碼
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ForgetPassword()
        {
            try
            {
                //Session["auth"] = false;
                return View("ForgetPassword");
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
        public JsonResult ForgetPassword(string forgetPassword)
        {
            try
            {
                dynamic forgetPasswordData = JsonConvert.DeserializeObject(forgetPassword);

                Login login = new Login()
                {
                    EMAIL = forgetPasswordData.Email.Value,
                    PASSWORD = forgetPasswordData.Password.Value
                };

                if (ModelState.IsValid)
                {
                    int determineLogin = loginService.ForgetPassword(login);
                    if (determineLogin == 0)
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