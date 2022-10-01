using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.Arg.BookMaintain
{
    //書籍類別檔				
    public class UpdatePost
    {
        /// <summary>
        /// PK 流水號
        /// </summary>
        [DisplayName("PK 流水號")]
        public int IDENTITY_FILED { get; set; }

        /// <summary>
        /// 類別代號
        /// </summary>
        [DisplayName("類別代號")]
        public int BOOK_ID { get; set; }

        /// <summary>
        /// 書名
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("書名")]
        public string BOOK_NAME { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [DisplayName("作者")]
        public string BOOK_AUTHOR { get; set; }

        /// <summary>
        /// 出版商
        /// </summary>
        [DisplayName("出版商")]
        public string BOOK_PUBLISHER { get; set; }

        /// <summary>
        /// 內容簡介
        /// </summary>
        [DisplayName("內容簡介")]
        public string BOOK_NOTE { get; set; }

        /// <summary>
        /// 購書日期
        /// </summary>
        [DisplayName("購書日期")]
        public string BOOK_BOUGHT_DATE { get; set; }

        /// <summary>
        /// 圖書類別
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("圖書類別")]
        public string BOOK_CLASS_ID { get; set; }

        /// <summary>
        /// 借閱狀態 (A可以借出 B以借出 U不可借出)
        /// </summary>
        [DisplayName("借閱狀態-書籍")]
        public string BOOK_STATUS { get; set; }

        /// <summary>
        /// 借閱人
        /// </summary>
        [DisplayName("借閱人")]
        public string USER_ID { get; set; }

        /// <summary>
        /// 借閱狀態
        /// </summary>
        [DisplayName("借閱狀態")]
        public string CODE_ID { get; set; }
    }
}
