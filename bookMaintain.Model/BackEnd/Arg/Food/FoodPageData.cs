using System.ComponentModel;
namespace bookMaintain.Model.BackEnd.Arg.Food
{				
    public class FoodPageData/*<T>*/
    {
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public List<bookMaintain.Model.BackEnd.Table.Food.Food> Data { get; set; }
        public string Error { get; set; }

        /*public FoodPageData()
        {
            Data = new List<T>();
        }*/
    }
}
