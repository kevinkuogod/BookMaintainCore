using bookMaintain.Model.BackEnd.Table.Food;

namespace bookMaintain.Service
{
    public interface IFoodService
    {
        Task<List<Food>> GetFood();
        List<Food> GetFoodMysql();
    }
}