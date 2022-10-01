using System.ComponentModel;
using System.Data.Entity;

namespace bookMaintain.Model.BackEnd.CodeFirst
{
    // BookContext.Categories為一個DbSet，可以用函式調用貌似list的概念
    public class BookContextTest : DbContext
    {
        public BookContextTest() : base("name=DBConn")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }
        //Category、Product可能是一張表出來的值
        public DbSet<BookData> BookData { get; set; }
        public DbSet<BookClass> BookClass { get; set; }
    }
}
