using bookMaintain.Model.BackEnd.Arg.Vocabulary;

namespace bookMaintain.Service
{
    public interface IVocabularyService
    {
        int InsertVocabulary(InsertArg insertArg);
    }
}