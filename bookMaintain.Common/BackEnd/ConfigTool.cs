using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Xml.Linq;

namespace bookMaintain.Common
{
    public class ConfigTool
    {
        public static IConfiguration Configuration { get; private set; } = null!;

        private readonly IConfiguration Configurations;

        public ConfigTool(IConfiguration configuration)
        {
            Configuration = configuration;
            Configurations = configuration;
        }

        public ConfigTool()
        {
        }

        public string GetConnectString(string tag)
        {
            //string test = Configurations["ConnectionStrings:DBConn"].ToString();
            string test = Configurations[tag].ToString();
            return test;
        }

        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        public static string GetDBConnectionString(string connectionStrings)
        {
            //從哪個專案進去，就用哪個專案的config
            ConfigurationBuilder builder = new ConfigurationBuilder();
            //builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            //builder.SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).AddJsonFile("appsettings.json");
            //builder.SetBasePath(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory())).AddJsonFile("appsettings.json");

            /*builder.SetBasePath(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString()).ToString()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            string DBConn = $"{configuration["ConnectionStrings:Default"]}";*/

            builder.SetBasePath(Directory.GetCurrentDirectory())
                //.AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false, true);
                //.AddEnvironmentVariables();
            var configuration = builder.Build();
            var collections = configuration.AsEnumerable();
            string DBConn = "";
            foreach (var item in collections)
            {
                if (item.Key.Equals(connectionStrings, StringComparison.CurrentCulture))
                {
                    DBConn = item.Value.ToString();
                    break;
                }
                Console.WriteLine("GetDBConnectionString{0}={1}", item.Key, item.Value);
            }
            //OnGet();
            //return Configuration.GetSection("ConnectionStrings:DBConn").Value;
            //var test = Configuration.GetConnectionString("DBConn");
            //var test = Configuration["ConnectionStrings:DBConn"];
            //var test = Configuration["DBConn"];
            //return Configuration["DBConn"];
            return DBConn;
        }

        public static string GetRedisConnectionString()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            /*builder.SetBasePath(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).AddJsonFile("appsettings.json");
            //Message = "The configuration file 'appsettings.json' was not found and is not optional. The expected physical path was 'D:\\C#\\appsettings.json'."
            var configuration = builder.Build();
            string DBConn = $"{configuration["ConnectionStrings:RedisConn"]}";*/

            builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false, true);
            var configuration = builder.Build();
            var collections = configuration.AsEnumerable();
            string DBConn = "";
            foreach (var item in collections)
            {
                if (item.Key.Equals("ConnectionStrings:RedisConn", StringComparison.CurrentCulture))
                {
                    DBConn = item.Value.ToString();
                    break;
                }
                Console.WriteLine("GetRedisConnectionString{0}={1}", item.Key, item.Value);
            }
            return DBConn;
        }

        public static string Getlog4netConfPathString()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            /*builder.SetBasePath(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString()).ToString()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            string DBConn = $"{configuration["appSettings:log4netConfPath"]}";*/

            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false, true);
            var configuration = builder.Build();
            var collections = configuration.AsEnumerable();
            string DBConn = "";
            foreach (var item in collections)
            {
                if (item.Key.Equals("appSettings:log4netConfPath", StringComparison.CurrentCulture))
                {
                    DBConn = item.Value.ToString();
                    break;
                }
                Console.WriteLine("Getlog4netConfPathString{0}={1}", item.Key, item.Value);
            }
            return DBConn;
        }
    }
}