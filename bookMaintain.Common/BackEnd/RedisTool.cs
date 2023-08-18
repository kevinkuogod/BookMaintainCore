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
        private string GetDBConnectionString()
        {
            return bookMaintain.Common.ConfigTool.GetDBConnectionString("ConnectionStrings:Default");
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
                    IDatabase db = redis.GetDatabase(dbMumber);
                    RedisValue bookNewNumberRS = db.StringGet("bookNewNumber");
                    string bookNewNumberS = bookNewNumberRS.ToString();
                    int bookNewNumber = 0;
                    if (string.IsNullOrEmpty(bookNewNumberS))
                    {
                        bookNewNumber = number;
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

        /// <summary>
        /// 輸入到redis陣列中
        /// </summary>
        /// <param name="dbMumber"></param>
        /// <param name="message"></param>
        public static void SetHashSet(int dbMumber = 0, string message = "")
        {
            try
            {
                using (ConnectionMultiplexer redis = GetRedisConnectionString())
                {
                    IDatabase db = redis.GetDatabase(dbMumber);
                    // 將陣列值存儲到Redis中的Hash
                    db.ListRightPush("mylist", message);
                }
            }
            catch (Exception parameterEx)
            {
                bookMaintain.Common.Logger.Write(bookMaintain.Common.Logger.LogCategoryEnum.Error, parameterEx.ToString());
            }
        }

        /// <summary>
        /// 搜尋消息對列中資訊
        /// </summary>
        /// <param name="dbMumber"></param>
        /// <param name="message"></param>
        public static List<string> SearchListRange(int dbMumber = 0)
        {
            try
            {
                using (ConnectionMultiplexer redis = GetRedisConnectionString())
                {
                    IDatabase db = redis.GetDatabase(dbMumber);
                    string[] retrievedValues = db.ListRange("mylist", 0, 10).ToStringArray();
                    return retrievedValues.ToList();
                }
            }
            catch (Exception parameterEx)
            {
                Logger.Write(Logger.LogCategoryEnum.Error, parameterEx.ToString());
                return new List<string>();
            }
        }

        /// <summary>
        /// 移除消息對列中資訊
        /// </summary>
        /// <param name="dbMumber"></param>
        /// <param name="message"></param>
        public static void DeleteListRange(int dbMumber = 0)
        {
            try
            {
                using (ConnectionMultiplexer redis = GetRedisConnectionString())
                {
                    IDatabase db = redis.GetDatabase(dbMumber);
                    string[] retrievedValues = db.ListRange("mylist", 0, 10).ToStringArray();
                    foreach (string value in retrievedValues)
                    {
                        db.ListRemove("mylist", value);
                    }
                }
            }
            catch (Exception parameterEx)
            {
                Logger.Write(Logger.LogCategoryEnum.Error, parameterEx.ToString());
            }
        }
    }
}