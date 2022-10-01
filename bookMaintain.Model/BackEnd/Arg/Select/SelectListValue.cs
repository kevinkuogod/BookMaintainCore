using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.Arg.Select
{
    //書籍類別檔				
    public class SelectListValue
    {
        /// <summary>
        /// 下拉式選單文字部分
        /// </summary>
        [DisplayName("下拉式選單文字部分")]
        public string Text { get; set; }

        /// <summary>
        /// 下拉式選單回傳值部分
        /// </summary>
        [DisplayName("下拉式選單回傳值部分")]
        public string Value { get; set; }
    }
}

