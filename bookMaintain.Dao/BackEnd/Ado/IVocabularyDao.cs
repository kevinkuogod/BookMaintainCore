using bookMaintain.Model.BackEnd.Arg.Vocabulary;

namespace bookMaintain.Dao.Ado
{
    public interface IVocabularyDao
    {
        int GetNetBookNumber();
        Task<int> InsertVocabulary(InsertArg insertArg);
    }
}