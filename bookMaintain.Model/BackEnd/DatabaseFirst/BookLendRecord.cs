using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.DatabaseFirst
{
    //書籍借閱紀錄檔				
    public class BookLendRecord
    {
        /// <summary>
        /// PK 流水號
        /// </summary>
        [DisplayName("PK 流水號")]
        public int IDENTITY_FILED { get; set; }

        /// <summary>
        /// 書籍ID
        /// </summary>
        [DisplayName("書籍ID")]
        public int BOOK_ID { get; set; }

        /// <summary>
        /// 借書者
        /// </summary>
        [DisplayName("借書者")]
        public string KEEPER_ID { get; set; }

        /// <summary>
        /// 借書時間
        /// </summary>
        [DisplayName("借書時間")]
        public string LEND_DATE { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [DisplayName("建立時間")]
        public string CRE_DATE { get; set; }

        /// <summary>
        /// 建立使用者
        /// </summary>
        [DisplayName("建立使用者")]
        public string CRE_USR { get; set; }

        /// <summary>
        /// 修改時間
        /// </summary>
        [DisplayName("修改時間")]
        public string MOD_DATE { get; set; }

        /// <summary>
        /// 修改使用者
        /// </summary>
        [DisplayName("修改使用者")]
        public string MOD_USR { get; set; }
    }
}
