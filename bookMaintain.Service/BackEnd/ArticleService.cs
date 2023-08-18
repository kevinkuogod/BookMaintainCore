using bookMaintain.Dao.Ado;
using bookMaintain.Dao.BackEnd.Ado;
using bookMaintain.Model.BackEnd.Table.Article;

namespace bookMaintain.Service
{
    /// <summary>
    /// kendo有自己的服務層，razor有自己的服務層
    /// </summary>
    /// <returns></returns>
    public class ArticleService : IArticleService
    {
        //private bookMaintain.Dao.IBookMaintainDao bookMaintainDao { get; set; }
        //private bookMaintain.Dao.IMockCodeDao mockCodeDao { get; set; }
        private IArticleDao articleDao;
        public ArticleService()
        {
            this.articleDao = new ArticleDao();
        }

        public async Task<int> EditorFile(IFormFile file)
        {
            return await articleDao.EditorFile(file);
        }

        public async Task<int> AddComment(string conmment)
        {
            return await articleDao.AddComment(conmment);
        }

        public async Task<List<Table2>> GetComment(/*string conmment*/)
        {
            return await articleDao.GetComment(/*conmment*/);
        }
    }
}