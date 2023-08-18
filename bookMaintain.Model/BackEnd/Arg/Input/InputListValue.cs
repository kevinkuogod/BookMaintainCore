using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.Arg.Input
{		
    public class InputListValue
    {
        /// <summary>
        /// 下拉式選單文字部分
        /// </summary>
        [DisplayName("下拉式選單文字部分")]
        public string? Text { get; set; }
    }
}
