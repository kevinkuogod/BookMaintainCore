using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.Table.BookLendRecord
{
    //書籍類別檔				
    public class Table
    {
        //*************************BookLendRecord********************************
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
        //*************************MemberM********************************
        /// <summary>
        /// 中文名稱
        /// </summary>
        [DisplayName("中文名稱")]
        public string USER_CNAME { get; set; }

        /// <summary>
        /// 英文名稱
        /// </summary>
        [DisplayName("英文名稱")]
        public string USER_ENAME { get; set; }
    }
}
