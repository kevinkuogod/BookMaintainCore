using bookMaintain.Model.BackEnd.Table.BookLendRecord;
using bookMaintain.Model.BackEnd.Arg.BookLendRecord;
using System.Collections.Generic;
using bookMaintain.Dao.BackEnd.Ado;

namespace bookMaintain.Service
{
    /// <summary>
    /// 書本借閱服務
    /// </summary>
    /// <returns></returns>
    public class BookLendRecordService : IBookLendRecordService
    {
        //private bookMaintain.Dao.IBookMaintainDao bookMaintainDao { get; set; }
        private IBookLendRecordDao bookLendRecordDao;
        public BookLendRecordService()
        {
            this.bookLendRecordDao = new BookLendRecordDao();
        }

        public List<Table> GetTable(SearchArg arg)
        {
            return bookLendRecordDao.GetTable(arg);
        }
    }
}