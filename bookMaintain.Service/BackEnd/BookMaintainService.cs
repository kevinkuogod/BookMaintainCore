//using bookMaintain.Dao.BackEnd.Ado;
using bookMaintain.Dao.BackEnd.Context;
using bookMaintain.Model.BackEnd.Arg.BookMaintain;

using bookMaintain.Model.BackEnd.Arg.Input;


using bookMaintain.Model.BackEnd.CodeFirst;

namespace bookMaintain.Service
{
    /// <summary>
    /// kendo有自己的服務層，razor有自己的服務層
    /// </summary>
    /// <returns></returns>
    public class BookMaintainService : IBookMaintainService
    { 
        private IBookMaintainDao bookMaintainDao { get; set; }
        //private bookMaintain.Dao.IBookMaintainDao bookMaintainDao { get; set; }
        //private bookMaintain.Dao.IMockCodeDao mockCodeDao { get; set; }
        //private IBookMaintainDao bookMaintainDao;
        /*public BookMaintainService(BookMaintainDao bookMaintainDao)
        {
             this.bookMaintainDao2 = bookMaintainDao;
        }*/
        public BookMaintainService()
        {
            this.bookMaintainDao = new BookMaintainDao();
            //this.bookMaintainDao = bookMaintainDao;
        }

        /// <summary>
        /// 需要換成Book換Table
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        /*public List<Book> GetTable(SearchArg arg)
        {
            return bookMaintainDao.GetTable(arg);
        }*/
        public List<Book> GetTable(SearchArg arg)
        {
            return bookMaintainDao.GetTable(arg);
        }

        /*public int InsertBookMaintain(InsertArg bookMaintain)
        {
            return bookMaintainDao.InsertBookMaintain(bookMaintain);
        }
        public int DeleteBookMaintain(int bookId)
        {
            return bookMaintainDao.DeleteBookMaintain(bookId);
        }
        public int UpdateBookMaintain(UpdatePost bookMaintainUpdatePost)
        {
            return bookMaintainDao.UpdateBookMaintain(bookMaintainUpdatePost);
        }

        /// <summary>
        /// 命名錯了需要統一所把服務寫的跟select依樣
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<InputListValue> GetInputTableBootstarp(string tableName, string type)
        {
            return bookMaintainDao.GetInputTableBootstarp(tableName, type);
        }

        /// <summary>
        /// 命名錯了需要統一所把服務寫的跟select依樣
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetNetBookNumber()
        {
            return bookMaintainDao.GetNetBookNumber();
        }*/
    }
}