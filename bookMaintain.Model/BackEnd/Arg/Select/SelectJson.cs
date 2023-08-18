using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.Arg.Select
{
    //書籍類別檔				
    public class SelectJson
    {
        /// <summary>
        /// select種類
        /// </summary>
        [DisplayName("select種類")]
        public string? type { get; set; }

    }
}
