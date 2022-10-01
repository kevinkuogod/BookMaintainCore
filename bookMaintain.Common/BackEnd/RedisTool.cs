using StackExchange.Redis;

namespace bookMaintain.Common
{
    public class RedisTool
    {
        /// <summary>
        /// 取得Redis連線字串
        /// </summary>
        /// <returns></returns>
        private static ConnectionMultiplexer GetRedisConnectionString()
        {
            return ConnectionMultiplexer.Connect(bookMaintain.Common.ConfigTool.GetRedisConnectionString());
        }

        public static int GetNetBookNumber(string bookNumberString)
        {
            int bookNewNumber = 0;
            try
            {
                using (ConnectionMultiplexer redis = GetRedisConnectionString())
                {
                    IDatabase db = redis.GetDatabase();
                    bookNewNumber = (int)db.StringGet(bookNumberString);
                    redis.Close();
                }
            }
            catch (Exception parameterEx)
            {
                bookMaintain.Common.Logger.Write(bookMaintain.Common.Logger.LogCategoryEnum.Error, parameterEx.ToString());
                return (int)bookMaintain.Common.ErrorCode.ErrorCodeField.redisError;
            }
            return bookNewNumber;
        }

        public static void SetNetBookNumber(int dbMumber = 0, int number = 0)
        {
            try
            {
                using (ConnectionMultiplexer redis = GetRedisConnectionString())
                {
                    IDatabase db               = redis.GetDatabase(dbMumber);
                    RedisValue bookNewNumberRS = db.StringGet("bookNewNumber");
                    string bookNewNumberS      = bookNewNumberRS.ToString();
                    int bookNewNumber          = 0;
                    if (string.IsNullOrEmpty(bookNewNumberS))
                    {
                        bookNewNumber =  number;
                    }
                    else
                    {
                        bookNewNumber = Convert.ToInt32(bookNewNumberS) + number;
                    }
                    db.StringSet("bookNewNumber", bookNewNumber.ToString());
                    redis.Close();
                }
            }
            catch (Exception parameterEx)
            {
                bookMaintain.Common.Logger.Write(bookMaintain.Common.Logger.LogCategoryEnum.Error, parameterEx.ToString());
            }
        }
    }
}