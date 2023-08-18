using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;
using bookMaintain.Model.BackEnd.Arg.BookMaintain;
using System.Net.Mail;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using bookMaintain.Model.BackEnd.Table.Article;
using System.Linq;
using Azure.Core;

namespace bookMaintain.Dao.BackEnd.Ado
{
    public class ArticleDao : IArticleDao
    {
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return bookMaintain.Common.ConfigTool.GetDBConnectionString("ConnectionStrings:Default");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>輸入圖書維護:大於0正確</returns>
        //
        public async Task<int> EditorFile(IFormFile file)
        {
            try
            {
                FileInfo fi = new FileInfo(file.FileName);
                string path = "C:\\Users\\kevin\\Desktop\\" + fi.Name;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> AddComment(string conmment)
        {
            if(conmment==null)
            {
                return 0;
            }
            try
            {
                string sql = @"INSERT INTO Article
        				 (
        					 ArticleTitle, ArticleContentEnglish, ArticleContentChinese, ArticleTopicEnglish,
                             ArticleTopicChinese, QuestionNumber, Editor,Article_Provenance,Audio_File,
                             Audio_Editor,Audio_Provenance,Privacy,Relative_Work,Created_At,Updated_At
        				 )
        				VALUES
        				(
        					 '作品集', '作品集N',@Conmment, '',
                             '', 0,'','','','','','','',@MODIFY_DATE,@MODIFY_DATE
        				)
                        SELECT SCOPE_IDENTITY()"
                ;
                int BookMaintainId = 0;
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlTransaction transaction;
                    transaction = conn.BeginTransaction();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    try
                    {
                        cmd.Transaction = transaction;
                        cmd.Parameters.Add(new SqlParameter("@Conmment", conmment));
                        cmd.Parameters.Add(new SqlParameter("@MODIFY_DATE", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")));
                        BookMaintainId = Convert.ToInt32(cmd.ExecuteScalar());
                        transaction.Commit();
                        conn.Close();
                    }
                    catch (Exception parameterEx)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception transactionEx)
                        {
                            bookMaintain.Common.Logger.Write(bookMaintain.Common.Logger.LogCategoryEnum.Error, transactionEx.ToString());
                            return (int)bookMaintain.Common.ErrorCode.ErrorCodeField.transactionError;
                        }
                        bookMaintain.Common.Logger.Write(bookMaintain.Common.Logger.LogCategoryEnum.Error, parameterEx.ToString());
                        return (int)bookMaintain.Common.ErrorCode.ErrorCodeField.SQLError;
                    }
                }
                return BookMaintainId;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<List<Table2>> GetComment(/*SearchArg arg*/)
        {
            DataSet bookDs = new DataSet();

            string bookData = @"SELECT [ID],
                                   [ArticleTitle],
                                   [ArticleContentEnglish],
                                   [ArticleContentChinese],
                                   [ArticleTopicEnglish],
                                   [ArticleTopicChinese],
                                   [QuestionNumber],
                                   [Editor],
                                   [Article_Provenance],
                                   [Audio_File],
                                   [Audio_Editor],
                                   [Audio_Provenance],
                                   [Privacy],
                                   [Relative_Work],
                                   [Created_At],
                                   [Updated_At] FROM [GSSWEB].[dbo].[Article];";

            /*var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("@BOOK_CLASS_ID", arg.BOOK_CLASS_ID));*/
            try
            {
                SqlConnection conn = new SqlConnection(this.GetDBConnectionString());
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(bookData, conn);
                Console.WriteLine(cmd.CommandText.ToString());
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(bookDs, "bookTable");
                conn.Close();
            }

            DataTable tables = bookDs.Tables["bookTable"];
                //var bookDtListPre = (from item in tables.AsEnumerable() select item).ToList();//執行LINQ語句,這裡的.AsEnumerable()是延遲發生,不會立即執行，實際上什麼都沒有發生
            var bookDtListPre = (from item in tables.AsEnumerable()
                                 select new Table2()
                                 {
                                     /*ID = Convert.ToInt32(tables.Rows[0]),
                                     ArticleTitle = tables.Rows[1].ToString(),
                                     ArticleContentEnglish = tables.Rows[2].ToString(),
                                     ArticleContentChinese = tables.Rows[3].ToString(),
                                     ArticleTopicEnglish = tables.Rows[4].ToString(),
                                     ArticleTopicChinese = tables.Rows[5].ToString(),
                                     QuestionNumber = Convert.ToInt32(tables.Rows[6]),
                                     Editor = tables.Rows[7].ToString(),
                                     Article_Provenance = tables.Rows[8].ToString(),
                                     Audio_File = tables.Rows[9].ToString(),
                                     Audio_Editor = tables.Rows[10].ToString(),
                                     Audio_Provenance = tables.Rows[11].ToString(),
                                     Privacy = tables.Rows[12].ToString(),
                                     Relative_Work = tables.Rows[13].ToString(),
                                     Created_At = tables.Rows[14].ToString(),
                                     Updated_At = tables.Rows[15].ToString(),*/

                                     /*ID = Convert.ToInt32(item.Field<int>(0)),
                                     ArticleTitle = item.Field<string>(1),
                                     ArticleContentEnglish = item.Field<string>(2),
                                     ArticleContentChinese = item.Field<string>(3),
                                     ArticleTopicEnglish = item.Field<string>(4),
                                     ArticleTopicChinese = item.Field<string>(5),
                                     QuestionNumber = Convert.ToInt32(item.Field<int>(6)),
                                     Editor = item.Field<string>(7),
                                     Article_Provenance = item.Field<string>(8),
                                     Audio_File = item.Field<string>(9),
                                     Audio_Editor = item.Field<string>(10),
                                     Audio_Provenance = item.Field<string>(11),
                                     Privacy = item.Field<string>(12),
                                     Relative_Work = item.Field<string>(13),
                                     Created_At = item.Field<string>(14),
                                     Updated_At = item.Field<string>(15),*/

                                     ID = Convert.ToInt32(item.Field<int>("ID")),
                                     ArticleTitle = item.Field<string>("ArticleTitle"),
                                     ArticleContentEnglish = item.Field<string>("ArticleContentEnglish"),
                                     ArticleContentChinese = item.Field<string>("ArticleContentChinese"),
                                     ArticleTopicEnglish = item.Field<string>("ArticleTopicEnglish"),
                                     ArticleTopicChinese = item.Field<string>("ArticleTopicChinese"),
                                     QuestionNumber = Convert.ToInt32(item.Field<int>("QuestionNumber")),
                                     Editor = item.Field<string>("Editor"),
                                     Article_Provenance = item.Field<string>("Article_Provenance"),
                                     Audio_File = item.Field<string>("Audio_File"),
                                     Audio_Editor = item.Field<string>("Audio_Editor"),
                                     Audio_Provenance = item.Field<string>("Audio_Provenance"),
                                     Privacy = item.Field<string>("Privacy"),
                                     Relative_Work = item.Field<string>("Relative_Work"),
                                     Created_At = item.Field<DateTime>("Created_At"),
                                     Updated_At = item.Field<DateTime>("Updated_At"),
                                     VisibleCheck = false

                                     /*ID = item["ID"],
                                     ArticleTitle = item["ArticleTitle"].ToString(),
                                     ArticleContentEnglish = item["ArticleContentEnglish"].ToString(),
                                     ArticleContentChinese = item["ArticleContentChinese"].ToString(),
                                     ArticleTopicEnglish = item["ArticleTopicEnglish"].ToString(),
                                     ArticleTopicChinese = item["ArticleTopicChinese"].ToString(),
                                     QuestionNumber = (int)item["QuestionNumber"],
                                     Editor = item["Editor"].ToString(),
                                     Article_Provenance = item["Article_Provenance"].ToString(),
                                     Audio_File = item["Audio_File"].ToString(),
                                     Audio_Editor = item["Audio_Editor"].ToString(),
                                     Audio_Provenance = item["Audio_Provenance"].ToString(),
                                     Privacy = item["Privacy"].ToString(),
                                     Relative_Work = item["Relative_Work"].ToString(),
                                     Created_At = item["Created_At"].ToString(),
                                     Updated_At = item["Updated_At"].ToString(),*/
                                 }).ToList();

            /*var bookDtListPre = (from item in tables.AsEnumerable()
                                 select item.Field<Table>()
                                 {
                                     ID = 1,
                                 }).ToList();
            products.Field<string>("ProductName")*/
            //List<ChatroomContent> bookDtList = this.bookDtToListArray(bookDtListPre);

            //釋放資源
            //bookDt.Clear();
            //bookDt.Dispose();

            return bookDtListPre;
                //return bookDtList;
            }
            catch (Exception ex)
            {
                return new List<Table2>();
            }
        }

        public delegate int BinaryOp(int x, int y);
        static void testInvoke()
        {
            Console.WriteLine("Main invoked on thread {0}.", Thread.CurrentThread.ManagedThreadId);
            BinaryOp b = new BinaryOp(add);
            //int answer = b(10, 10);//隱含觸發委託
            int answer = b.Invoke(10, 10);//顯示觸發委託

            Console.WriteLine("Doing more work in Main");
            Console.WriteLine("10 + 10 is {0}", answer);
        }

        static int add(int x, int y)
        {
            Console.WriteLine("add invoked on thread {0}.", Thread.CurrentThread.ManagedThreadId);
            return x + y;
        }
    }
}