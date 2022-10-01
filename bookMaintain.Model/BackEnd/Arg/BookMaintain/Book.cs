using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.Arg.BookMaintain
{
    //書籍資料檔				
    public class Book
    {
        /// <summary>
        /// 類別代號
        /// </summary>
        [DisplayName("類別代號")]
        public int BOOK_ID { get; set; }

        /// <summary>
        /// 類別名稱
        /// </summary>
        [DisplayName("書籍名稱")]
        public string BOOK_NAME { get; set; }

        /// <summary>
        /// 書籍購書日期
        /// </summary>
        [DisplayName("書籍購書日期")]
        public string BOOK_BOUGHT_DATE { get; set; }

        /// <summary>
        /// 代碼Id
        /// </summary>
        [DisplayName("代碼Id")]
        public string CODE_ID { get; set; }

        /// <summary>
        /// 類別代號BOOK_CLASS.BOOK_CLASS_ID
        /// </summary>
        [DisplayName("類別代號BOOK_CLASS.BOOK_CLASS_ID")]
        public string BOOK_CLASS_ID { get; set; }

        /// <summary>
        /// 書籍作者
        /// </summary>
        [DisplayName("書籍作者")]
        public string BOOK_AUTHOR { get; set; }

        /// <summary>
        /// 內容簡介
        /// </summary>
        [DisplayName("內容簡介")]
        public string BOOK_NOTE { get; set; }

        /// <summary>
        /// 出版商
        /// </summary>
        [DisplayName("出版商")]
        public string BOOK_PUBLISHER { get; set; }

        /// <summary>
        /// PK 流水號
        /// </summary>
        [DisplayName("PK 流水號")]
        public int IDENTITY_FILED { get; set; }

        /// <summary>
        /// 人員編號
        /// </summary>
        [DisplayName("人員編號")]
        public string USER_ID { get; set; }

        /// <summary>
        /// 類別名稱
        /// </summary>
        [DisplayName("類別名稱")]
        public string BOOK_CLASS_NAME { get; set; }

        /// <summary>
        /// CodeId描述
        /// </summary>
        [DisplayName("CodeId描述")]
        public string CODE_NAME { get; set; }

        /// <summary>
        /// 英文名稱
        /// </summary>
        [DisplayName("英文名稱")]
        public string USER_ENAME { get; set; }

        /// <summary>
        /// 書籍購買金額
        /// </summary>
        [DisplayName("書籍購買金額")]
        public int BOOK_AMOUNT { get; set; }
    }
}
