using Microsoft.AspNetCore.Authentication.Cookies;

namespace BookMaintainCore
{
    public static class Startup
    {
        public static WebApplication InitalizeApp(string[] args)
        {
            //.net5 時會用到
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = true;
            });

            //加了這個之後驗會配上 [Authorize]，有家的都會被導到login
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                //options.Cookie.Name = "UserLoginCookie";
                options.AccessDeniedPath = "/Login/ForgetPassword"; //權限
                options.LoginPath = "/Login/Index"; //驗證登入
                options.LogoutPath = "/Login/Index"; //驗證登出
                //↓資安建議false，白箱弱掃軟體會要求cookie不能延展效期，這時設false變成絕對逾期時間
                //↓如果你的客戶反應明明一直在使用系統卻容易被自動登出的話，你再設為true(然後弱掃policy請客戶略過此項檢查)
                options.SlidingExpiration = false;

                options.ReturnUrlParameter = "returnUrl";
                //options.ReturnUrlParameter = "ret";

                //options.ExpireTimeSpan = TimeSpan.FromDays(1);
                //options.ExpireTimeSpan = new TimeSpan(72, 0, 0);//cookie的过期时间会72小时
                //options.Cookie.HttpOnly = true;
                //options.Cookie.SameSite = SameSiteMode.Lax;
                //options.Cookie.IsEssential = true;
                //options.CookieManager = new ChunkingCookieManager();

            }
            );
            /*.AddPolicy("RequireManagerOnly", policy =>
              policy.RequireRole("Manager", "Administrator"));*/
            
            ConfiguareServices(builder);
            var app = builder.Build();
            Configure(app);
            return app;
        }

        private static void ConfiguareServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews();
           
        }

        private static void Configure(WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();//添加会话中间件

            app.UseAuthorization();//授權

            app.UseAuthentication();//驗證

            app.MapControllerRoute(
                name: "default",
                //pattern: "{controller=Home}/{action=Index}/{id?}");
                pattern: "{controller=BookMaintain}/{action=Index}/{id?}");
        }
    }
}
