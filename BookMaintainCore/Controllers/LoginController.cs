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
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json.Linq;
using Azure.Core;
using System.Reflection.Metadata;
using System.Drawing;
//using System.Web;

namespace BookMaintain.Controllers
{
    [Route("backend/[controller]")]
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
        //[HttpGet]
        [HttpGet("Index")]
        public ActionResult Index()
        {
            try
            {
                CreateRandom createRandomObj = new CreateRandom();
                ViewBag.code = createRandomObj.createRandomString();
                //HttpContext.Current.Session["LoginName"] = loginCode;
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
        [HttpGet("Logout")]
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
        //[HttpPost]
        [HttpPost("Insert")]
        //[EnableCors("MyPolicy")]
        [DisableCors]
        //public JsonResult Insert(Login loginJson)
        public async Task<JsonResult> Insert(Login loginJson)
        {
            try
            {
                Logger.Write(Logger.LogCategoryEnum.Error, "13");
                Logger.Write(Logger.LogCategoryEnum.Error, loginJson.Email);
                Logger.Write(Logger.LogCategoryEnum.Error, "33");
                if (ModelState.IsValid)
                {
                    var loginObject = loginService.Insert(loginJson);
                    if (loginObject.loginStatus == 1)
                    {
                        //Session["auth"] = true;
                        /*
                         //取用戶信息
                         var userId = User.FindFirst(ClaimTypes.Sid).Value;
                         var userName = User.Identity.Name;
                         */
                        /*//之後要開，new Claim(ClaimTypes.Sid,info.Id.ToString()), //用戶ID
                         var claims = new List<Claim>() {
                           new Claim(ClaimTypes.NameIdentifier,"test"),
                           new Claim(ClaimTypes.Name,loginJson.Email.ToString()),
                           new Claim(ClaimTypes.Role, "Administrator")
                       };*/
                        //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    

                        /*之後要開
                         * var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsIdentity = new ClaimsIdentity(
                                           claims, CookieAuthenticationDefaults.AuthenticationScheme);*/
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

                        /*之後要開
                         * HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                        new ClaimsPrincipal(claimsIdentity),
                                                        authProperties);*/
                        return this.Json(new { type = "login", message = "登入成功", loginName = loginObject.loginName });
                 
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
                return new JsonHttpStatusResult(
                           new { type = "login", message = parameterEx.ToString(), code = (int)ErrorCode.ErrorCodeField.insertBookError }
                            , HttpStatusCode.InternalServerError);
                /*Logger.Write(Logger.LogCategoryEnum.Error, parameterEx.ToString());
                return new JsonHttpStatusResult(
                           new { type = "login", message = "登入過程錯誤", code = (int)ErrorCode.ErrorCodeField.insertBookError }
                            , HttpStatusCode.InternalServerError);*/
            }
        }

        /// <summary>
        /// 來源 IP
        /// </summary>
        /// <returns></returns>
        public string GetClientIP()
        {
            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            return remoteIpAddress.ToString();
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
                    Email = forgetPasswordData.Email.Value,
                    Password = forgetPasswordData.Password.Value
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

        [HttpGet]
        public JsonResult GetLoninNumber()
        {
            int loginNumber = loginService.LoginNumber();
            return this.Json(new { type = "LoninNumber", message = loginNumber });
        }

        [HttpGet("GetValidateCode")]
        public ActionResult GetValidateCode(string loginCode)
        {
            CreateRandom createRandomObj = new CreateRandom();
            byte[] data = null;

            string code = loginCode;
            //定義一個畫板
            MemoryStream ms = new MemoryStream();
            using (Bitmap map = new Bitmap(150, 40))
            {
                //畫筆,在指定畫板畫板上畫圖
                //g.Dispose();
                using (Graphics g = Graphics.FromImage(map))
                {
                    g.Clear(Color.White);
                    g.DrawString(code, new Font("黑體", 18.0F), Brushes.Blue, new Point(10, 8));
                    //繪製干擾線(數字代表幾條)
                    createRandomObj.PaintInterLine(g, 10, map.Width, map.Height);
                }
                map.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            data = ms.GetBuffer();
            return File(data, "image/jpeg");
        }
    }
}