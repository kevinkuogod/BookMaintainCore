using System.Collections.Generic;
using bookMaintain.Model.BackEnd.CodeFirst;

namespace bookMaintain.Model.BackEnd.Seed
{
    // BookContext.Categories為一個DbSet，可以用函式調用貌似list的概念
    public class BookSeed
    {
        protected void Seed(BookContextTest context)
        {
            GetCategories().ForEach(c => context.BookData.Add(c));
        }

        private static List<BookData> GetCategories()
        {
            var categories = new List<BookData> {
                    new BookData
                    {
                        BOOK_ID = 1,
                        BOOK_NAME = "Cars"
                    }
                };

            return categories;
        }
    }
}
