using bookMaintain.Model.BackEnd.Arg.BookMaintain;
using bookMaintain.Model.BackEnd.Table.Article;
namespace bookMaintain.Service
{
    public interface IArticleService
    {
        Task<int> EditorFile(IFormFile file);

        Task<int> AddComment(string conmment);
        Task<List<Table2>> GetComment(/*string conmment*/);
    }
}