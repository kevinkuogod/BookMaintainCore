namespace BookMaintainCore
{
    public static class Startup
    {
        public static WebApplication InitalizeApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                //pattern: "{controller=Home}/{action=Index}/{id?}");
                pattern: "{controller=BookMaintainController}/{action=Index}/{id?}");
        }
    }
}
