using System.Collections.Generic;
using System.Data;
using System;
using System.Linq;
using bookMaintain.Model.BackEnd.CodeFirst;
using bookMaintain.Model.BackEnd.Arg.BookMaintain;
using bookMaintain.Model.BackEnd.Arg.Input;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace bookMaintain.Dao.BackEnd.Ado
{
    public class BookMaintainDao : IBookMaintainDao
    {
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return bookMaintain.Common.ConfigTool.GetDBConnectionString();
        }

        public int GetNetBookNumber()
        {
            return bookMaintain.Common.RedisTool.GetNetBookNumber("bookNewNumber");
        }

        /// <summary>
        /// 取得GetBookDataByCondtioin的全部資料
        /// </summary>
        /// <returns></returns>
        public List<Book> GetTable(SearchArg arg)
        {
            //leftJon版，對照組，叡揚問的leftjoin版
            DataSet bookDs = new DataSet();
            //DataTable bookDt = new DataTable();

            /*string bookData = @"SELECT [BOOK_NAME],
                                       CONVERT(varchar(10), [BOOK_BOUGHT_DATE], 111) as [BOOK_BOUGHT_DATE],
                                       [BOOK_STATUS],
                                       BOOK_DATA.[BOOK_ID],
                                       [BOOK_NOTE],
                                       [BOOK_AUTHOR],
                                       [BOOK_AMOUNT],
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
                                       WHERE BOOK_CODE.[CODE_TYPE]='BOOK_STATUS'
                                       AND Book_Class.BOOK_CLASS_ID = @BOOK_CLASS_ID";*/

            string bookData = @"SELECT [BOOK_NAME],
                                       CONVERT(varchar(10), [BOOK_BOUGHT_DATE], 111) as [BOOK_BOUGHT_DATE],
                                       [BOOK_STATUS],
                                       BOOK_DATA.[BOOK_ID],
                                       [BOOK_NOTE],
                                       [BOOK_AUTHOR],
                                       [BOOK_AMOUNT],
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

            /*string bookData = @"SELECT [BOOK_NAME],
                                       CONVERT(varchar(10), [BOOK_BOUGHT_DATE], 111) as [BOOK_BOUGHT_DATE],
                                       [BOOK_STATUS],
                                       [BOOK_ID],
                                       [BOOK_CLASS_ID],
                                       [BOOK_NOTE],
                                       [BOOK_AUTHOR],
                                       [BOOK_PUBLISHER]
                                       FROM BOOK_DATA WHERE BOOK_CLASS_ID = @BOOK_CLASS_ID";*/


            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("@BOOK_CLASS_ID", arg.BOOK_CLASS_ID));
            list.Add(new KeyValuePair<string, string>("@CODE_ID", arg.CODE_ID));
            list.Add(new KeyValuePair<string, string>("@BOOK_NAME", arg.BOOK_NAME));
            list.Add(new KeyValuePair<string, string>("@USER_ID", arg.USER_ID));

            SqlConnection conn      = new SqlConnection(this.GetDBConnectionString());
            bool BOOK_CLASS_ID_BOOL = arg.BOOK_CLASS_ID != null;
            bool CODE_ID_BOOL       = arg.CODE_ID != null;
            bool BOOK_NAME_BOOL     = arg.BOOK_NAME != null;
            bool USER_ID_BOOL       = arg.USER_ID != null;
            using (conn)
            {
                conn.Open();
                SqlCommand cmd;
                if (BOOK_CLASS_ID_BOOL)
                {
                    bookData = bookData + " AND Book_Class.BOOK_CLASS_ID = @BOOK_CLASS_ID";
                }

                if (CODE_ID_BOOL)
                {
                    bookData = bookData + " AND CODE_ID = @CODE_ID";
                }

                if (BOOK_NAME_BOOL)
                {
                   bookData = bookData + " AND BOOK_NAME = @BOOK_NAME";
                }

                if (USER_ID_BOOL)
                {
                    bookData = bookData + " AND USER_ID = @USER_ID";
                }

                bookData = bookData + " Order By [BOOK_BOUGHT_DATE] DESC;";
                cmd = new SqlCommand(bookData, conn);
                foreach (var element in list)
                {
                    if (bookData.IndexOf(element.Key) != -1)
                    {
                        cmd.Parameters.Add(new SqlParameter(element.Key, element.Value));
                    }
                }
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);

                sqlAdapter.Fill(bookDs, "bookTable");
                //sqlAdapter.Fill(bookDt);
                conn.Close();
            }

            //List <Book> bookDtList = this.bookDtToList(bookDt);
            //需要看一下
            
            DataTable tables = bookDs.Tables["bookTable"];
            var bookDtListPre = (from item in tables.AsEnumerable() select item).ToList();//執行LINQ語句,這裡的.AsEnumerable()是延遲發生,不會立即執行，實際上什麼都沒有發生
            List<Book> bookDtList = this.bookDtToListArray(bookDtListPre);
            
            //釋放資源
            //bookDt.Clear();
            //bookDt.Dispose();

            return bookDtList;
            //return bookDtList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>輸入圖書維護:大於0正確</returns>
        //
        public int InsertBookMaintain(InsertArg insertArg)
        {
            string sql = @"INSERT INTO BOOK_DATA
        				 (
        					 BOOK_NAME, BOOK_CLASS_ID, BOOK_AUTHOR, BOOK_BOUGHT_DATE,
                             BOOK_PUBLISHER, BOOK_NOTE, BOOK_STATUS,CREATE_DATE,CREATE_USER,
                             MODIFY_DATE,MODIFY_USER,BOOK_KEEPER,BOOK_AMOUNT
        				 )
        				VALUES
        				(
        					 @BOOK_NAME, @BOOK_CLASS_ID,@BOOK_AUTHOR, @BOOK_BOUGHT_DATE,
                             @BOOK_PUBLISHER, @BOOK_NOTE,'A',@CREATE_DATE,'180',
                             @MODIFY_DATE,'180','',''
        				)
                        SELECT SCOPE_IDENTITY()";

            int BookMaintainId = 0;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlTransaction transaction;
                transaction = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand(sql, conn);
                bookMaintain.Common.RedisTool.SetNetBookNumber(0, 1);
                try
                {
                    cmd.Transaction = transaction;
                    cmd.Parameters.Add(new SqlParameter("@BOOK_NAME", insertArg.BOOK_NAME));
                    cmd.Parameters.Add(new SqlParameter("@BOOK_CLASS_ID", insertArg.BOOK_CLASS_ID));
                    cmd.Parameters.Add(new SqlParameter("@BOOK_AUTHOR", insertArg.BOOK_AUTHOR));
                    cmd.Parameters.Add(new SqlParameter("@BOOK_BOUGHT_DATE", insertArg.BOOK_BOUGHT_DATE));
                    cmd.Parameters.Add(new SqlParameter("@BOOK_PUBLISHER", insertArg.BOOK_PUBLISHER));
                    cmd.Parameters.Add(new SqlParameter("@BOOK_NOTE", insertArg.BOOK_NOTE));
                    cmd.Parameters.Add(new SqlParameter("@CREATE_DATE", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")));
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

        /// <summary>
		/// 刪除圖書維護資料
		/// </summary>
		public int DeleteBookMaintain(int bookId)
        {
            string deleteBookData       = "Delete FROM BOOK_DATA Where BOOK_ID=@BOOK_ID";
            string deleteBookLendRecode = "Delete FROM BOOK_LEND_RECORD Where BOOK_ID=@BOOK_ID";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlTransaction transaction;
                transaction = conn.BeginTransaction();
                SqlCommand cmdBookData, cmdBookLendRecode;
                try
                {
                    cmdBookData = new SqlCommand(deleteBookData, conn);
                    cmdBookData.Transaction = transaction;
                    cmdBookData.Parameters.Add(new SqlParameter("@BOOK_ID", bookId));
                    cmdBookData.ExecuteNonQuery();
                    cmdBookLendRecode = new SqlCommand(deleteBookLendRecode, conn);
                    cmdBookLendRecode.Transaction = transaction;
                    cmdBookLendRecode.Parameters.Add(new SqlParameter("@BOOK_ID", bookId));
                    cmdBookLendRecode.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception sqlEx)
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
                    bookMaintain.Common.Logger.Write(bookMaintain.Common.Logger.LogCategoryEnum.Error, sqlEx.ToString());
                    return (int)bookMaintain.Common.ErrorCode.ErrorCodeField.SQLError;
                }
                conn.Close();
            }
            return (int)bookMaintain.Common.CorrectCode.CorrectCodeField.SQLCorrect;
        }
        public int UpdateBookMaintain(UpdatePost updateBookMaintainPost)
        {
            string insertBookData = "";
            string updateBookLendRecode = "";
            string searchBookData = "";
            int determine_group_status = 0;

            //選擇前Model資料驗證
            switch (updateBookMaintainPost.CODE_ID)
            {
                case "A":
                case "U":
                    updateBookLendRecode = @"UPDATE BOOK_DATA
                                                   SET
                                                       BOOK_STATUS = @CODE_ID,
                                                       BOOK_KEEPER = '' 
                                                  WHERE BOOK_ID = @BOOK_ID";
                    break;
                case "B":
                case "C":
                    insertBookData = @"INSERT INTO BOOK_LEND_RECORD
                                                 (BOOK_ID,
                                                  KEEPER_ID, 
                                                  LEND_DATE, 
                                                  CRE_USR, 
                                                  CRE_DATE, 
                                                  MOD_DATE, 
                                                  MOD_USR
                                                  ) 
                                                  VALUES
                                                  (@BOOK_ID,  
                                                  @USER_ID,  
                                                  @BOOK_BOUGHT_DATE, 
                                                  @USER_ID, 
                                                  @BOOK_BOUGHT_DATE, 
                                                  @BOOK_BOUGHT_DATE, 
                                                  @USER_ID)";
                    //只憑借閱紀錄表無法得知查詢，一定得搭配書本資料表
                    searchBookData = @"SELECT BOOK_STATUS 
                                                 FROM BOOK_DATA 
                                                 WHERE BOOK_ID = @BOOK_ID";

                    updateBookLendRecode = @"UPDATE BOOK_DATA 
                                                   SET BOOK_STATUS = @CODE_ID 
                                                 WHERE BOOK_ID = @BOOK_ID";
                    determine_group_status = 2;
                    break;
            }


            DataTable searchDt = new DataTable();
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlTransaction transaction;
                SqlCommand insertCmd, updateCmd, searchCmd;
                transaction = conn.BeginTransaction(System.Data.IsolationLevel.Snapshot);
                string searchBookStatus = "";
                //如果借閱人相同書本ID相同又是已借出狀態就不要借了
                //回傳值要寫成工具
                //選擇後Model資料驗證
                try
                {
                    if (determine_group_status == 2)
                    {
                        searchCmd = new SqlCommand(searchBookData, conn);
                        searchCmd.Transaction = transaction;
                        searchCmd.Parameters.Add(new SqlParameter("@BOOK_ID", updateBookMaintainPost.BOOK_ID));
                        SqlDataAdapter searchCmdAdapter = new SqlDataAdapter(searchCmd);
                        searchCmdAdapter.Fill(searchDt);
                        searchBookStatus = searchDt.Rows[0][0].ToString();
                        if ((!string.IsNullOrEmpty(searchBookStatus))
                            && ((searchBookStatus.Equals("B", StringComparison.CurrentCulture)) || (searchBookStatus.Equals("C", StringComparison.CurrentCulture)))
                            && ((updateBookMaintainPost.CODE_ID.Equals("B", StringComparison.CurrentCulture)) || (updateBookMaintainPost.CODE_ID.Equals("C", StringComparison.CurrentCulture))))
                        {
                            return (int)bookMaintain.Common.ErrorCode.ErrorCodeField.bookLendError;
                        }
                    }

                    if (!string.IsNullOrEmpty(insertBookData))
                    {
                        insertCmd = new SqlCommand(insertBookData, conn);
                        insertCmd.Transaction = transaction;
                        insertCmd.Parameters.Add(new SqlParameter("@BOOK_ID", updateBookMaintainPost.BOOK_ID));
                        insertCmd.Parameters.Add(new SqlParameter("@USER_ID", updateBookMaintainPost.USER_ID));
                        insertCmd.Parameters.Add(new SqlParameter("@BOOK_BOUGHT_DATE", updateBookMaintainPost.BOOK_BOUGHT_DATE));
                        insertCmd.ExecuteNonQuery();
                    }
                    updateCmd = new SqlCommand(updateBookLendRecode, conn);
                    updateCmd.Transaction = transaction;
                    updateCmd.Parameters.Add(new SqlParameter("@CODE_ID", updateBookMaintainPost.CODE_ID));
                    updateCmd.Parameters.Add(new SqlParameter("@BOOK_ID", updateBookMaintainPost.BOOK_ID));
                    updateCmd.ExecuteNonQuery();
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
            return (int)bookMaintain.Common.CorrectCode.CorrectCodeField.SQLCorrect;
        }

        /// <summary>
        /// Maping 代碼資料
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<Book> bookDtToListArray(List<DataRow> bookDtListPre)
        {
            List<Book> result = new List<Book>();
            foreach (var res in bookDtListPre)
            {
                result.Add(
                    new Book()
                    {
                        BOOK_ID = (int)res["BOOK_ID"],
                        BOOK_NAME = string.IsNullOrEmpty(res["BOOK_NAME"].ToString()) ? "*此書無書名*" : res["BOOK_NAME"].ToString(),
                        BOOK_BOUGHT_DATE = res["BOOK_BOUGHT_DATE"].ToString(),
                        CODE_ID = res["CODE_ID"].ToString(),
                        BOOK_CLASS_ID = res["BOOK_CLASS_ID"].ToString(),
                        BOOK_AUTHOR = res["BOOK_AUTHOR"].ToString(),
                        BOOK_NOTE = res["BOOK_NOTE"].ToString(),
                        BOOK_PUBLISHER = res["BOOK_PUBLISHER"].ToString(),
                        IDENTITY_FILED = string.IsNullOrEmpty(res["IDENTITY_FILED"].ToString()) ? 0 : (int)res["IDENTITY_FILED"],
                        USER_ID = string.IsNullOrEmpty(res["KEEPER_ID"].ToString()) ? "*此書無作者ID*" : res["KEEPER_ID"].ToString(),
                        BOOK_CLASS_NAME = res["BOOK_CLASS_NAME"].ToString(),
                        CODE_NAME = res["CODE_NAME"].ToString(),
                        USER_ENAME = string.IsNullOrEmpty(res["USER_ENAME"].ToString()) ? "*此書無作者*" : res["USER_ENAME"].ToString(),
                        BOOK_AMOUNT = (int)res["BOOK_AMOUNT"]
                    }
                );
            }
            return result;
        }

        /// <summary>
        /// Maping 代碼資料
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<Book> bookDtToList(DataTable dt)
        {
            List<Book> result = new List<Book>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(
                    new Book()
                    {
                        BOOK_ID = (int)row["BOOK_ID"],
                        BOOK_NAME = string.IsNullOrEmpty(row["BOOK_NAME"].ToString()) ? "*此書無書名*" : row["BOOK_NAME"].ToString(),
                        BOOK_BOUGHT_DATE = row["BOOK_BOUGHT_DATE"].ToString(),
                        CODE_ID = row["CODE_ID"].ToString(),
                        BOOK_CLASS_ID = row["BOOK_CLASS_ID"].ToString(),
                        BOOK_AUTHOR = row["BOOK_AUTHOR"].ToString(),
                        BOOK_NOTE = row["BOOK_NOTE"].ToString(),
                        BOOK_PUBLISHER = row["BOOK_PUBLISHER"].ToString(),
                        IDENTITY_FILED = string.IsNullOrEmpty(row["IDENTITY_FILED"].ToString()) ? 0 : (int)row["IDENTITY_FILED"],
                        USER_ID = string.IsNullOrEmpty(row["KEEPER_ID"].ToString()) ? "*此書無作者ID*" : row["KEEPER_ID"].ToString(),
                        BOOK_CLASS_NAME = row["BOOK_CLASS_NAME"].ToString(),
                        CODE_NAME = row["CODE_NAME"].ToString(),
                        USER_ENAME = string.IsNullOrEmpty(row["USER_ENAME"].ToString()) ? "*此書無作者*" : row["USER_ENAME"].ToString(),
                    }
                );
            }
            return result;
        }

        /// <summary>
        /// Maping 代碼資料
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<BookLendRecord> MapBookLendRecordToList(DataTable dt, string type)
        {
            List<BookLendRecord> result = new List<BookLendRecord>();
            foreach (DataRow row in dt.Rows)
            {
                this.AddBookLendRecordData(result, row, type);
            }
            return result;
        }

        private List<BookLendRecord> AddBookLendRecordData(List<BookLendRecord> result, DataRow row, string type)
        {
            switch (type)
            {
                case "BookMaintain":
                    result.Add(
                        new BookLendRecord()
                        {
                            IDENTITY_FILED = (int)row["IDENTITY_FILED"],
                            BOOK_ID = (int)row["BOOK_ID"],
                            KEEPER_ID = row["KEEPER_ID"].ToString()
                        }
                    );
                    break;
                case "BookLendRecor":
                    result.Add(
                        new BookLendRecord()
                        {
                            KEEPER_ID = row["KEEPER_ID"].ToString(),
                            LEND_DATE = row["LEND_DATE"].ToString()
                        }
                    );
                    break;
            }
            return result;
        }

        /// <summary>
        /// Maping 代碼資料
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<MemberM> MapMemberMToList(DataTable dt, string type)
        {
            List<MemberM> result = new List<MemberM>();
            foreach (DataRow row in dt.Rows)
            {
                this.AddMemberMData(result, row, type);
            }
            return result;
        }

        private List<MemberM> AddMemberMData(List<MemberM> result, DataRow row, string type)
        {
            switch (type)
            {
                case "BookMaintain":
                    result.Add(
                        new MemberM()
                        {
                            USER_ID = row["USER_ID"].ToString(),
                            USER_ENAME = row["USER_ENAME"].ToString(),
                        }
                    );
                    break;
                case "BookLendRecor":
                    result.Add(
                        new MemberM()
                        {
                            USER_ID = row["USER_ID"].ToString(),
                            USER_ENAME = row["USER_ENAME"].ToString(),
                            USER_CNAME = row["USER_CNAME"].ToString()
                        }
                    );
                    break;
            }
            return result;
        }

        /// <summary>
        /// GetInputTableBootstarp 處理書本AutoComplete對資料庫的撈取
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="type"></param>
        /// <returns>InputListValue</returns>
        public List<InputListValue> GetInputTableBootstarp(string tableName, string type)
        {
            DataTable dt = new DataTable();
            string sql = "";
            switch (tableName)
            {
                case "BOOK_DATA":
                    sql = @"SELECT BOOK_NAME
                            FROM BOOK_DATA
                            GROUP BY BOOK_NAME";
                    break;
            }

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapInputData(dt, tableName, type);
        }

        /// <summary>
        /// MapInputData 處理書本回傳資料
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tableName"></param>
        /// <param name="type"></param>
        /// <returns>InputListValue</returns>
        private List<InputListValue> MapInputData(DataTable dt, string tableName, string type)
        {
            List<InputListValue> result = new List<InputListValue>();
            foreach (DataRow row in dt.Rows)
            {
                switch (tableName)
                {
                    case "BOOK_DATA":
                        result.Add(new InputListValue()
                        {
                            Text = row["BOOK_NAME"].ToString()
                        });
                        break;
                }
            }
            return result;
        }
    }
}