using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.Arg.BookMaintain
{
    public class Register
    {
        /// <summary>
        /// 顧客'姓'後名稱
        /// </summary>
        [DisplayName("顧客姓後名稱")]
        public string FIRST_NAME { get; set; }
        
        /// <summary>
        /// 顧客'姓'名稱
        /// </summary>
        [DisplayName("顧客姓名稱")]
        public string LAST_NAME { get; set; }

        /// <summary>
        /// 信箱
        /// </summary>
        [DisplayName("信箱")]
        public string EMAIL { get; set; }

        /// <summary>
        /// 電話名稱
        /// </summary>
        [DisplayName("電話名稱")]
        public string TELEPHONE { get; set; }

        /// <summary>
        /// 傳真
        /// </summary>
        [DisplayName("傳真")]
        public string FAX { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [DisplayName("密碼")]
        public string PASSWORD { get; set; }
    }
}
