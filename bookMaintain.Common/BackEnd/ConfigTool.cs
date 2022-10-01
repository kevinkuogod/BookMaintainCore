using Microsoft.AspNetCore.Mvc;
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

        public string OnGet()
        {
            string test = Configurations["ConnectionStrings:DBConn"].ToString();
            return test;
        }

        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        public static string GetDBConnectionString()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var collections = configuration.AsEnumerable();
            string DBConn = "";
            foreach (var item in collections)
            {
                if (item.Key.Equals("ConnectionStrings:DBConn",StringComparison.CurrentCulture))
                {
                    DBConn = item.Value.ToString();
                }
                Console.WriteLine("{0}={1}", item.Key, item.Value);
            }
            //OnGet();
            //return Configuration.GetSection("ConnectionStrings:DBConn").Value;
            //var test = Configuration.GetConnectionString("DBConn");
            //var test = Configuration["ConnectionStrings:DBConn"];
            //return Configuration["ConnectionStrings:DBConn"];
            return DBConn;
        }

        public static string GetRedisConnectionString()
        {
            return Configuration["ConnectionStrings:RedisConn"];
        }

        public static string Getlog4netConfPathString()
        {
            return Configuration["appSettings:log4netConfPath"];
        }
    }
}