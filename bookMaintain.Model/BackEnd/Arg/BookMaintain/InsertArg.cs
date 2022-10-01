using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.Arg.BookMaintain
{
    //書籍類別檔				
    public class InsertArg
    {
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

    }
}
