using bookMaintain.Model.BackEnd.Table.Food;

namespace bookMaintain.Dao.BackEnd.Ado
{
    public interface IFoodDao
    {
        Task<int> CutFood(string conmment);
        Task<List<Food>> GetFood();
        List<Food> GetFoodMysql();
    }
}