using bookMaintain.Model.BackEnd.Arg.BookMaintain;
using bookMaintain.Model.BackEnd.Arg.Input;
using System.Collections.Generic;

namespace bookMaintain.Service
{
    public interface IBookMaintainService
    {
        int DeleteBookMaintain(int bookId);
        List<InputListValue> GetInputTableBootstarp(string tableName, string type);
        List<Book> GetTable(SearchArg arg);
        int InsertBookMaintain(InsertArg bookMaintain);
        int UpdateBookMaintain(UpdatePost bookMaintainUpdatePost);
        int GetNetBookNumber();
    }
}