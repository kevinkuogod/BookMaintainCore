namespace bookMaintain.Common
{
    public class ErrorCode
    {
        /// <summary>
        /// ErrorCode
        /// </summary>
        /// <returns></returns>
        public enum ErrorCodeField : int
        {
            SQLError = -1, //SQL錯誤
            bookLendError = -2, //借書錯誤
            formSelectError = -3, //下拉式選單錯誤
            formInputError = -4,   //輸入選單錯誤
            tableMainError = -5,  //主表取得過程錯誤
            insertBookError = -6, //輸入書本資料錯誤
            updateBookError = -7, //更新書本資料錯誤
            deleteBookError = -8, //刪除書本資料錯誤
            transactionError = -9, //刪除書本資料錯誤
            redisError = -10, //redis錯誤
        }
    }

    public class CorrectCode
    {
        /// <summary>
        /// CorrectCode
        /// </summary>
        /// <returns></returns>
        public enum CorrectCodeField : int
        {
            SQLCorrect = 0, //SQL錯誤
        }
    }
}