using bookMaintain.Model.BackEnd.Arg.Vocabulary;

namespace bookMaintain.Dao.Ado
{
    public interface IVocabularyDao
    {
        int GetNetBookNumber();
        int InsertVocabulary(InsertArg insertArg);
    }
}