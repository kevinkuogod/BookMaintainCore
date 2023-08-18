using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.Arg.Input
{
    //書籍類別檔				
    public class InputJson
    {
        /// <summary>
        /// input種類
        /// </summary>
        [DisplayName("input種類")]
        public string? type { get; set; }
    }
    /*
    public class InsertGroupA
    {
        public string paramA1 { get; set; }
        public string paramA2 { get; set; }
    }
    public class InsertGroupB
    {
        public string paramB1 { get; set; }
        public string paramB2 { get; set; }
    }

    public class JsonPostModel
    {
        List<InsertGroupA> GroupA { get; set; }
        List<InsertGroupB> GroupB { get; set; }
        public string Type { get; set; }
        public string Product { get; set; }
    }
    */
}
