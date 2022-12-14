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
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
//Microsoft.EntityFrameworkCore

namespace bookMaintain.Dao.BackEnd.Context
{
    public class BookMaintainDao
    {
        /// <summary>
        /// 取得GetBookDataByCondtioin的全部資料
        /// 積極式載入
        /// </summary>
        /// <returns></returns>
        public List<BookData> GetTable(SearchArg arg)
        {
            List<BookData> bookData = null;
            using (BookContext context = new BookContext())
            {
                //Include需要System.Data.Entity

                List<BookData> bookDatas = context.BookData
                    .Include(b => b.BookClass.Where(z => z.BOOK_CLASS_ID.Equals("1",System.StringComparison.Ordinal))
                                             .OrderByDescending(z => z.BOOK_CLASS_ID)
                                             .Take(5))
                    //.Where(w => w.BOOK_ID > 5)
                    //.Include(b => b.BOOK_NAME)
                    //.Include(x => x.BookClass.SelectMany(z => z.BOOK_CLASS_ID)) //多個層級
                    //可以關聯多張表
                    //上面那行等於.ThenInclude(post => post.BOOK_NAME)
                    //.OrderBy(z => z.BOOK_CLASS_ID)
                    //.Take(5)  //只取五筆
                    .ToList();

                //IEnumerable<DataRow> AsEnumerable() 需要轉list要再看看
                //IOrderedQueryable<BookData> blogs2
                /*
                List<BookData> rows = (from b in context.BookData.AsEnumerable()
                                                      where b.BOOK_ID < 4
                                                      orderby b.BOOK_NAME
                                                      select b).ToList();
                */

                //要吃model的轉型
                //blogs2.AsQueryable
                //blogs2.AsEnumerable()
                //blogs2.DataRow["SecondNumber"];

                //.Navigation(e => e.ColorScheme).AutoInclude(); //他人自定義的方法，它的效果 Include 與在結果中傳回實體類型的每個查詢中的導覽相同。
                //.IgnoreAutoIncludes() //他人自定義的方法，使用此方法將會停止載入使用者設定為自動包含的所有導覽。 
            }
            return bookData;

            /*
            var blogs2 = (from b in context.BookData
                          where b.BOOK_ID < 4
                          orderby b.BOOK_NAME
                          select b);
            */

            /*
            //leftJon版，對照組，叡揚問的leftjoin版
            DataTable bookDt = new DataTable();

            string bookData = @"SELECT [BOOK_NAME],
                                       CONVERT(varchar(10), [BOOK_BOUGHT_DATE], 111) as [BOOK_BOUGHT_DATE],
                                       [BOOK_STATUS],
                                       BOOK_DATA.[BOOK_ID],
                                       [BOOK_NOTE],
                                       [BOOK_AUTHOR],
                                       [BOOK_PUBLISHER],
                                       BOOK_CODE.[CODE_TYPE],
                                       BOOK_CODE.[CODE_ID],
                                       BOOK_CODE.[CODE_NAME],
                                       BOOK_CODE.[CODE_TYPE_DESC],
                                       Book_Class.[BOOK_CLASS_ID],
                                       Book_Class.[BOOK_CLASS_NAME],
                                       BOOK_LEND_RECORD.[KEEPER_ID],
                                       BOOK_LEND_RECORD.[IDENTITY_FILED],
                                       MEMBER_M.[USER_ENAME]
                                       FROM BOOK_DATA
                                       LEFT JOIN Book_Class ON Book_Class.[BOOK_CLASS_ID] = BOOK_DATA.[BOOK_CLASS_ID]
                                       LEFT JOIN BOOK_CODE ON BOOK_CODE.[CODE_ID] = BOOK_DATA.[BOOK_STATUS]
                                       LEFT JOIN BOOK_LEND_RECORD ON BOOK_LEND_RECORD.[BOOK_ID] = BOOK_DATA.[BOOK_ID]
                                       LEFT JOIN MEMBER_M ON MEMBER_M.[USER_ID] = BOOK_LEND_RECORD.[KEEPER_ID]
                                       WHERE BOOK_CODE.[CODE_TYPE]='BOOK_STATUS'";


            SqlConnection conn = new SqlConnection(this.GetDBConnectionString());
            bool BOOK_CLASS_ID_BOOL = arg.BOOK_CLASS_ID != null;
            bool CODE_ID_BOOL = arg.CODE_ID != null;
            bool BOOK_NAME_BOOL = arg.BOOK_NAME != null;
            bool USER_ID_BOOL = arg.USER_ID != null;

            using (conn)
            {
                conn.Open();
                SqlCommand cmd;
                if (BOOK_CLASS_ID_BOOL)
                {
                    bookData = bookData + " AND BOOK_CLASS_ID = @BOOK_CLASS_ID";
                    cmd = new SqlCommand(bookData, conn);
                    cmd.Parameters.Add(new SqlParameter("@BOOK_CLASS_ID", BOOK_CLASS_ID_BOOL ? arg.BOOK_CLASS_ID : string.Empty));
                }

                if (CODE_ID_BOOL)
                {
                    bookData = bookData + " AND CODE_ID = @CODE_ID";
                    cmd = new SqlCommand(bookData, conn);
                    cmd.Parameters.Add(new SqlParameter("@CODE_ID", CODE_ID_BOOL ? arg.CODE_ID : string.Empty));
                }

                if (BOOK_NAME_BOOL)
                {
                    bookData = bookData + " AND BOOK_NAME = @BOOK_NAME Order By [BOOK_BOUGHT_DATE] DESC";
                    cmd = new SqlCommand(bookData, conn);
                    cmd.Parameters.Add(new SqlParameter("@BOOK_NAME", BOOK_NAME_BOOL ? arg.BOOK_NAME : string.Empty));
                }

                if (BOOK_NAME_BOOL)
                {
                    bookData = bookData + " AND BOOK_NAME = @BOOK_NAME Order By [BOOK_BOUGHT_DATE] DESC;";
                    cmd = new SqlCommand(bookData, conn);
                    cmd.Parameters.Add(new SqlParameter("@BOOK_NAME", BOOK_NAME_BOOL ? arg.BOOK_NAME : string.Empty));
                }

                if (USER_ID_BOOL)
                {
                    bookData = bookData + " AND USER_ID = @USER_ID";
                    cmd = new SqlCommand(bookData, conn);
                    cmd.Parameters.Add(new SqlParameter("@USER_ID", USER_ID_BOOL ? arg.USER_ID : string.Empty));
                }

                bookData = bookData + ";";
                cmd = new SqlCommand(bookData, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(bookDt);
                conn.Close();
            }

            List<Book> bookDtList = this.bookDtToList(bookDt);
            //釋放資源
            bookDt.Clear();
            bookDt.Dispose();

            return bookDtList;
            */
        }

        /// <summary>
        /// 取得GetBookDataByCondtioin的全部資料
        /// 消極式載入，命名要重新改一下
        /// </summary>
        /// <returns></returns>
        public List<BookData> GetTable1(SearchArg arg)
        {
            List<BookData> bookData = null;
            using (BookContext context = new BookContext())
            {
                //Include需要System.Data.Entity

                BookData bookData1 = context.BookData
                    .Single(b => b.BOOK_ID == 1);

                //.Collection(b => b.Posts)
                //    .Load() 
                //.Reference(b => b.Owner)
                //.Query(b => b.BOOK_ID == 1)
            }
            return bookData;
        }

        /// <summary>
        /// 取得GetBookDataByCondtioin的全部資料
        /// 消極式載入，命名要重新改一下
        /// </summary>
        /// <returns></returns>
        public List<BookData> GetTable2(SearchArg arg)
        {
            List<BookData> bookData = null;
            using (BookContext context = new BookContext())
            {
                //Include需要System.Data.Entity

                //BookData bookData1 = context.BookData
                //    .UseLazyLoadingProxies()
                //    .UseSqlServer(myConnectionString);

                
            }
            return bookData;
        }
    }
}