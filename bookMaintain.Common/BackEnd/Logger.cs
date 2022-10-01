using System.IO;
namespace bookMaintain.Common
{
    public class Logger
    {

        public enum LogCategoryEnum
        {
            Information,
            Error,
            Warning
        }

        static log4net.ILog? log4netInstance;
        public Logger()
        {


        }
        public static void Write(LogCategoryEnum logCatogroy, string context)
        {
            log4netInstance = log4net.LogManager.GetLogger("Looger");
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Common.ConfigTool.Getlog4netConfPathString()));
            switch (logCatogroy)
            {
                case LogCategoryEnum.Information:
                    log4netInstance.Info(context);
                    break;
                case LogCategoryEnum.Error:
                    log4netInstance.Error(context);
                    break;
                case LogCategoryEnum.Warning:
                    log4netInstance.Warn(context);
                    break;
                default:
                    break;
            }

        }
    }
}
