using bookMaintain.Model.BackEnd.Arg.Vocabulary;

namespace bookMaintain.Service
{
    public interface IVocabularyService
    {
        Task<int> InsertVocabulary(InsertArg insertArg);
    }
}