using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.Arg.Food
{				
    public class DataTablesRequest
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        // 可以添加其他需要的参数
    }
}
