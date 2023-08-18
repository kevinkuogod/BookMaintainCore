using BookMaintainCore.Job;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.FileProviders;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.IsEssential = true;
});

var MyAllowSpecificOrigins = "CorsPolicy";
builder.Services.AddCors(options =>
{
    /*options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://www.sample.com",
                            "http://www.demo.com");
    });*/
    // CorsPolicy 是自訂的 Policy 名稱
    options.AddPolicy(MyAllowSpecificOrigins, policy =>
    {
        //policy.WithOrigins("http://localhost:4200", "http://localhost:8100") 不可用
        //policy.WithOrigins("http://localhost:8100")
        //policy.WithOrigins("http://192.168.2.25:8100", "http://localhost:8100", "https://kevinkuotesttwo.ddns.net")
        //policy.WithOrigins("http://192.168.2.25:8100", "https://kevinkuotesttwo.ddns.net")
        //policy.WithOrigins("https://kevinkuotest.ddns.net")
        //policy.WithOrigins("https://localhost")
        /*policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();*/
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddMvc();

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
    //options.Cookie.HttpOnly = true;//使Cookies只能允許在Http傳輸使用
    //options.Cookie.SameSite = SameSiteMode.Lax;
    //options.Cookie.IsEssential = true;
    //options.CookieManager = new ChunkingCookieManager();

}
);
/*.AddPolicy("RequireManagerOnly", policy =>
  policy.RequireRole("Manager", "Administrator"));*/

builder.Services.AddControllersWithViews();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionScopedJobFactory();
    // Just use the name of your job that you created in the Jobs folder.
    var jobKey = new JobKey("SendWebSocketJob");
    q.AddJob<SendWebSocketJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("SendWebSocketJob-trigger")
        //This Cron interval can be described as "run every minute" (when second is zero)
        .WithCronSchedule("0/30 * * ? * *")
        // 0/2 * * * * ? 每兩秒
        // 0 0/2 * * * ? 表示每2分钟
        // 0 15 10 ? * *    每天上午10:15触发 
    );
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

//IIS靜態檔案
/*builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});*/

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);//跨域
//app.UseCors();

app.UseAuthorization();//授權
app.UseAuthentication();//驗證

var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};
app.UseWebSockets(webSocketOptions);

//跨域設定來源，目前部會設設定
app.UseEndpoints(endpoints =>
{
    endpoints.MapPost("/login",
        context => context.Response.WriteAsync("login"))
        .RequireCors(MyAllowSpecificOrigins);

    //endpoints.MapGet("/echo",
    //    context => context.Response.WriteAsync("echo"))
    //    .RequireCors(MyAllowSpecificOrigins);

    endpoints.MapControllers()
             .RequireCors(MyAllowSpecificOrigins);
    //目前沒用mvc服務
});
/*app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});*/

app.UseSession();//添加会话中间件

//IIS靜態檔案
/*app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "MyStaticFiles")),
    RequestPath = "/StaticFiles"
});*/

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{controller=BookMaintain}/{action=Index}/{id?}");
app.Run();
