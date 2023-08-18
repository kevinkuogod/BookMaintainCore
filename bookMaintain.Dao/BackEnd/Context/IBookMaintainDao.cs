/*
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using bookMaintain.Model;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
*/
using bookMaintain.Model.BackEnd.Arg.BookMaintain;
using bookMaintain.Model.BackEnd.CodeFirst;

namespace bookMaintain.Dao.BackEnd.Context
{
    public interface IBookMaintainDao
    {
        List<ChatroomContent> GetTable(SearchArg arg);
        List<BookData> GetTable1(SearchArg arg);
        List<BookData> GetTable2(SearchArg arg);
    }
}