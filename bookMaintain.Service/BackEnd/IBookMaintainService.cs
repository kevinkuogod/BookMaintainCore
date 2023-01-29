using bookMaintain.Model.BackEnd.Arg.BookMaintain;
using bookMaintain.Model.BackEnd.Arg.Input;
using System.Collections.Generic;

using bookMaintain.Model.BackEnd.CodeFirst;

namespace bookMaintain.Service
{
    public interface IBookMaintainService
    {
        //List<Book> GetTable(SearchArg arg);
        List<Book> GetTable(SearchArg arg);
        
        /*List<InputListValue> GetInputTableBootstarp(string tableName, string type);
        int InsertBookMaintain(InsertArg bookMaintain);
        int UpdateBookMaintain(UpdatePost bookMaintainUpdatePost);
        int DeleteBookMaintain(int bookId);
        int GetNetBookNumber();*/
    }
}