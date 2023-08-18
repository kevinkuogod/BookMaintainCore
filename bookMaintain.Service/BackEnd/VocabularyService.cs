using System.Collections.Generic;
using bookMaintain.Dao.Ado;
using bookMaintain.Model.BackEnd.Arg.Vocabulary;

namespace bookMaintain.Service
{
    /// <summary>
    /// kendo有自己的服務層，razor有自己的服務層
    /// </summary>
    /// <returns></returns>
    public class VocabularyService : IVocabularyService
    {
        //private bookMaintain.Dao.IBookMaintainDao bookMaintainDao { get; set; }
        //private bookMaintain.Dao.IMockCodeDao mockCodeDao { get; set; }
        private IVocabularyDao vocabularyDao;
        public VocabularyService()
        {
            this.vocabularyDao = new VocabularyDao();
        }

        /// <summary>
        /// 需要換成Book換Table
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public async Task<int> InsertVocabulary(InsertArg insertArg)
        {
            return await vocabularyDao.InsertVocabulary(insertArg);
        }
    }
}