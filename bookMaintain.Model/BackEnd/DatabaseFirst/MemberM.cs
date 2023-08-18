using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.DatabaseFirst
{
    //人員檔				
    public class MemberM
    {
        /// <summary>
        /// 人員編號
        /// </summary>
        [DisplayName("人員編號")]
        public string? USER_ID { get; set; }

        /// <summary>
        /// 中文名稱
        /// </summary>
        [DisplayName("中文名稱")]
        public string? USER_CNAME { get; set; }

        /// <summary>
        /// 英文名稱
        /// </summary>
        [DisplayName("英文名稱")]
        public string? USER_ENAME { get; set; }

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
