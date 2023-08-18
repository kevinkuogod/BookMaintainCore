using bookMaintain.Model.BackEnd.Table.BookMaintain;
using System;
using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.Table.Article
{
    //書籍類別檔				
    public class Table
    {
        public int ID { get; set; }
        public string? ArticleTitle { get; set; }
        public string? ArticleContentEnglish { get; set; }
        public string? ArticleContentChinese { get; set; }
        public string? ArticleTopicEnglish { get; set; }
        public string? ArticleTopicChinese { get; set; }
        public int QuestionNumber { get; set; }
        public string? Editor { get; set; }
        public string? Article_Provenance { get; set; }
        public string? Audio_File { get; set; }
        public string? Audio_Editor { get; set; }
        public string? Audio_Provenance { get; set; }
        public string? Privacy { get; set; }
        public string? Relative_Work { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }

    public class Table2 : Table
    {
        public bool VisibleCheck { get; set; }
    }
}
