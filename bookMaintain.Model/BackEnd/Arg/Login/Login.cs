using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.Arg.BookMaintain
{
    public class Login
    {
        /// <summary>
        /// 信箱
        /// </summary>
        [DisplayName("信箱")]
        public string? Email { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [DisplayName("密碼")]
        public string? Password { get; set; }

        /// <summary>
        /// 機器種類
        /// </summary>
        /*[DisplayName("機器種類")]
        public string? MachineType { get; set; }*/
    }
}
