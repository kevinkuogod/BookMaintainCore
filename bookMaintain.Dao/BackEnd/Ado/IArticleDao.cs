using bookMaintain.Model.BackEnd.Table.Article;

namespace bookMaintain.Dao.BackEnd.Ado
{
    public interface IArticleDao
    {
        Task<int> EditorFile(IFormFile file);
        Task<int> AddComment(string conmment);
        Task<List<Table2>> GetComment(/*string conmment*/);
    }
}