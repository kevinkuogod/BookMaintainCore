using System.Data.Entity;

namespace bookMaintain.Model.BackEnd.CodeFirst
{
    public interface IBookContext
    {
        DbSet<BookClass> BookClass { get; set; }
        DbSet<BookData> BookData { get; set; }
        int SaveChangesCount { get; }

        int SaveChanges();
    }
}