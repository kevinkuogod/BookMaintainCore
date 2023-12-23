using bookMaintain.Model.BackEnd.Arg.Food;
using bookMaintain.Model.BackEnd.Table.Food;

namespace bookMaintain.Dao.BackEnd.Ado
{
    public interface IFoodDao
    {
        Task<int> CutFood(string conmment);
        Task<List<Food>> GetFood();
        FoodPageData GetFoodPageMysql(DataTablesRequest FoodPage);
        List<Food> GetFoodMysql();
        List<FoodOrderQuantity> GetFoodOrderQuantity();
        int BuyFood(dynamic buyFood);
    }
}