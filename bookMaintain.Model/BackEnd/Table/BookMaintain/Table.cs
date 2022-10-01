using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.Table.BookMaintain
{
    //書籍類別檔				
    public class Table
    {
        //*************************BookClass********************************
        /// <summary>
        /// 類別名稱
        /// </summary>
        [DisplayName("類別名稱")]
        public string BOOK_CLASS_NAME { get; set; }
        //*************************BookCode********************************
        /// <summary>
        /// 代碼id
        /// </summary>
        [DisplayName("代碼id")]
        public string CODE_ID { get; set; }

        /// <summary>
        /// CODE ID描述
        /// </summary>
        [DisplayName("CODE ID描述")]
        public string CODE_NAME { get; set; }
        //*************************BookData********************************
        /// <summary>
        /// 書籍名稱
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("書籍名稱")]
        public string BOOK_NAME { get; set; }

        /// <summary>
        /// 書籍購書日期
        /// </summary>
        [DisplayName("書籍購書日期")]
        public string BOOK_BOUGHT_DATE { get; set; }

        /// <summary>
        /// 狀態BOOK_CODE.CODE_ID (A可以借出 B以借出 U不可借出)
        /// </summary>
        [DisplayName("狀態(A可以借出 B以借出 U不可借出)")]
        public string BOOK_STATUS { get; set; }

        /// <summary>
        /// 類別代號
        /// </summary>
        [DisplayName("類別代號")]
        public int BOOK_ID { get; set; }

        /// <summary>
        /// 類別代號BOOK_CLASS.BOOK_CLASS_ID
        /// </summary>
        [DisplayName("類別代號")]
        public string BOOK_CLASS_ID { get; set; }

        /// <summary>
        /// 書籍作者
        /// </summary>
        [DisplayName("書籍作者")]
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
        //*************************BookLendRecord********************************
        /// <summary>
        /// PK 流水號
        /// </summary>
        [DisplayName("PK 流水號")]
        public int IDENTITY_FILED { get; set; }

        //*************************MemberM********************************
        /// <summary>
        /// 人員編號
        /// </summary>
        [DisplayName("人員編號")]
        public string USER_ID { get; set; }

        /// <summary>
        /// 英文名稱
        /// </summary>
        [DisplayName("英文名稱")]
        public string USER_ENAME { get; set; }
    }
}
