using bookMaintain.Model.BackEnd.Arg.BookMaintain;
using bookMaintain.Model.BackEnd.Arg.Input;
using System.Collections.Generic;

namespace bookMaintain.Dao.BackEnd.Ado
{
    public interface IBookMaintainDao
    {
        int DeleteBookMaintain(int bookId);
        List<InputListValue> GetInputTableBootstarp(string tableName, string type);
        List<ChatroomContent> GetTable(SearchArg arg);
        int InsertBookMaintain(InsertArg insertArg);
        int UpdateBookMaintain(UpdatePost updateBookMaintainPost);
        int GetNetBookNumber();
    }
}