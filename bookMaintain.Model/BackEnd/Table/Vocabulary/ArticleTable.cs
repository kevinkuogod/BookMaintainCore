using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;

namespace bookMaintain.Model.BackEnd.Table.BookMaintain
{
    //文章繼承單字項				
    public class ArticleTable : Table
    { 

        /// <summary>
        /// 文章id
        /// </summary>
        [DisplayName("文章id")]
        public string? ID { get; set; }

        /// <summary>
        /// 文章內容，不可為空
        /// </summary>
        [DisplayName("文章內容")]
        public string? ArticleContent { get; set; }

        /// <summary>
        /// 文章作者，可為空因為可能很古老
        /// </summary>
        [DisplayName(" 文章作者")]
        public string? Editor { get; set; }

        /// <summary>
        /// 文章出處，可為空因為可能很古老
        /// </summary>
        ///[MaxLength(5)]
        [DisplayName("文章出處")]
        public string? Article_Provenance { get; set; }

        /// <summary>
        /// 音檔，可為空因為可能很沒有
        /// </summary>
        [DisplayName("音檔")]
        public string? Audio_File { get; set; }

        /// <summary>
        /// 音檔作者
        /// </summary>
        [DisplayName("音檔作者")]
        public string? Audio_Editor { get; set; }

        /// <summary>
        /// 音檔出處，可為空因為可能很古老
        /// </summary>
        [DisplayName("音檔出處")]
        public int Audio_Provenance { get; set; }

        /// <summary>
        /// 可為空因為可能很古老
        /// </summary>
        [DisplayName("公開與否")]
        public string? Privacy { get; set; }

        /// <summary>
        /// 文章引用，可為空因為可能很古老
        /// </summary>
        [DisplayName("文章引用")]
        public string? Relative_Work { get; set; }

        /// <summary>
        /// 更改使用者創建時間
        /// </summary>
        [DisplayName("更改使用者創建時間")]
        public string? Created_At { get; set; }

        /// <summary>
        /// 更改使用者更新時間
        /// </summary>
        [DisplayName("更改使用者更新時間")]
        //public string? Updated_At { get; set; }
        public string? Updated_At { get; set; }
    }
}
