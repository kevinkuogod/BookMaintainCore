using bookMaintain.Model.BackEnd.Arg.BookLendRecord;
using bookMaintain.Model.BackEnd.Table.BookLendRecord;
using System.Collections.Generic;

namespace bookMaintain.Service
{
    public interface IBookLendRecordService
    {
        List<Table> GetTable(SearchArg arg);
    }
}