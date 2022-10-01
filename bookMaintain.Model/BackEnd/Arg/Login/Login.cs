using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.Arg.BookMaintain
{
    public class Login
    {
        /// <summary>
        /// 信箱
        /// </summary>
        [DisplayName("信箱")]
        public string EMAIL { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [DisplayName("密碼")]
        public string PASSWORD { get; set; }
    }
}
