using bookMaintain.Model.BackEnd.Arg.Food;
using bookMaintain.Model.BackEnd.Table.Food;

namespace bookMaintain.Service
{
    public interface IFoodService
    {
        Task<List<Food>> GetFood();
        FoodPageData GetFoodPageMysql(DataTablesRequest FoodPage);
        List<Food> GetFoodMysql();

        List<FoodOrderQuantity> GetFoodOrderQuantity();
        int BuyFood(dynamic buyFood);
    }
}