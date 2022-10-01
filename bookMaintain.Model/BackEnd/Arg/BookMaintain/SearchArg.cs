using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.Arg.BookMaintain
{
    //書籍類別檔				
    public class SearchArg
    {
        /// <summary>
        /// 書名
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("書名")]
        public string BOOK_NAME { get; set; }

        /// <summary>
        /// 圖書類別BOOK_CLASS.BOOK_CLASS_ID
        /// </summary>
        [DisplayName("圖書類別")]
        public string BOOK_CLASS_ID { get; set; }

        /// <summary>
        /// 借閱人
        /// </summary>
        [DisplayName("借閱人")]
        public string USER_ID { get; set; }

        /// <summary>
        /// 代碼Id
        /// </summary>
        [DisplayName("借閱狀態")]
        public string CODE_ID { get; set; }

        /// <summary>
        /// 分頁大小
        /// </summary>
        [DisplayName("分頁大小")]
        public string pageSize { get; set; }
    }
}
