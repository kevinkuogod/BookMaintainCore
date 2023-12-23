using bookMaintain.Dao.Ado;
using bookMaintain.Dao.BackEnd.Ado;
using bookMaintain.Model.BackEnd.Arg.Food;
using bookMaintain.Model.BackEnd.Table.Food;

namespace bookMaintain.Service
{
    /// <summary>
    /// kendo有自己的服務層，razor有自己的服務層
    /// </summary>
    /// <returns></returns>
    public class FoodService : IFoodService
    {
        private IFoodDao articleDao;
        public FoodService()
        {
            this.articleDao = new FoodDao();
        }

        /*public async Task<int> EditorFile(IFormFile file)
        {
            return await articleDao.EditorFile(file);
        }

        public async Task<int> AddComment(string conmment)
        {
            return await articleDao.AddComment(conmment);
        }*/

        public async Task<List<Food>> GetFood(/*string conmment*/)
        {
            return await articleDao.GetFood(/*conmment*/);
        }
        
        public FoodPageData GetFoodPageMysql(DataTablesRequest FoodPage)
        {
            return articleDao.GetFoodPageMysql(FoodPage);
        }
        public List<Food> GetFoodMysql()
        {
            return articleDao.GetFoodMysql();
        }

        public List<FoodOrderQuantity> GetFoodOrderQuantity()
        {
            return articleDao.GetFoodOrderQuantity();
        }

        public int BuyFood(dynamic buyFood)
        {
            return articleDao.BuyFood(buyFood);
        }
    }
}