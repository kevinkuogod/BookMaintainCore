using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.DatabaseFirst
{
    //書籍代碼檔				
    public class BookCode
    {
        /// <summary>
        /// 代碼型態
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("代碼型態")]
        public string? CODE_TYPE { get; set; }

        /// <summary>
        /// 代碼Id
        /// </summary>
        [DisplayName("代碼Id")]
        public string? CODE_ID { get; set; }

        /// <summary>
        /// CodeType描述
        /// </summary>
        [DisplayName("CodeType描述")]
        public string? CODE_TYPE_DESC { get; set; }

        /// <summary>
        /// CodeId描述
        /// </summary>
        [DisplayName("CodeId描述")]
        public string? CODE_NAME { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [DisplayName("建立時間")]
        public string? CREATE_DATE { get; set; }

        /// <summary>
        /// 建立使用者
        /// </summary>
        [DisplayName("建立使用者")]
        public string? CREATE_USER { get; set; }


        /// <summary>
        /// 修改時間
        /// </summary>
        [DisplayName("修改時間")]
        public string? MODIFY_DATE { get; set; }

        /// <summary>
        /// 修改使用者
        /// </summary>
        [DisplayName("修改使用者")]
        public string? MODIFY_USER { get; set; }
    }
}