using bookMaintain.Model.BackEnd.Arg.BookLendRecord;
using bookMaintain.Model.BackEnd.Table.BookLendRecord;
using System.Collections.Generic;

namespace bookMaintain.Dao.BackEnd.Ado
{
    public interface IBookLendRecordDao
    {
        List<Table> GetTable(SearchArg arg);
    }
}