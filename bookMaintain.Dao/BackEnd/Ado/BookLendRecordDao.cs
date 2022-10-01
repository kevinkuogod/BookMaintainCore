using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;
using bookMaintain.Model.BackEnd.Table.BookLendRecord;
using bookMaintain.Model.BackEnd.Arg.BookLendRecord;
using bookMaintain.Model.BackEnd.CodeFirst;

namespace bookMaintain.Dao.BackEnd.Ado
{
    public class BookLendRecordDao : IBookLendRecordDao
    {
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return bookMaintain.Common.ConfigTool.GetDBConnectionString();
        }

        /// <summary>
        /// 取得Redis連線字串
        /// </summary>
        /// <returns></returns>
        private string GetRedisConnectionString()
        {
            return bookMaintain.Common.ConfigTool.GetRedisConnectionString();
        }

        /// <summary>
        /// 取得GetBookLendRecordDataByCondtioin的全部資料
        /// 換為GetTable
        /// </summary>
        /// <returns></returns>
        public List<Table> GetTable(SearchArg arg)
        {
            DataTable bookLendRecordDt = new DataTable();
            DataTable memberMDt = new DataTable();

            string bookLendRecord = @"SELECT 
                                      [KEEPER_ID],
                                      CONVERT(varchar(10), [LEND_DATE], 111) as [LEND_DATE]
                                      FROM BOOK_LEND_RECORD";

            string memberM = @"SELECT [USER_ID], 
                                      [USER_ENAME], 
                                      [USER_CNAME] 
                                      FROM MEMBER_M;";

            SqlConnection conn = new SqlConnection(this.GetDBConnectionString());
            bool BOOK_ID_BOOL = arg.BOOK_ID != null;

            using (conn)
            {
                conn.Open();
                SqlCommand cmd;
                if (BOOK_ID_BOOL)
                {
                    bookLendRecord = bookLendRecord + " WHERE BOOK_ID = @BOOK_ID;";
                    cmd = new SqlCommand(bookLendRecord, conn);
                    cmd.Parameters.Add(new SqlParameter("@BOOK_ID", BOOK_ID_BOOL ? arg.BOOK_ID : string.Empty));
                }
                else
                {
                    bookLendRecord = bookLendRecord + ";";
                    cmd = new SqlCommand(bookLendRecord, conn);
                }
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(bookLendRecordDt);

                cmd = new SqlCommand(memberM, conn);
                sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(memberMDt);

                conn.Close();
            }

            List<BookLendRecord> bookLendRecordList = this.MapBookLendRecordToList(bookLendRecordDt, "BookLendRecor");
            List<MemberM> memberMList = this.MapMemberMToList(memberMDt, "BookLendRecor");

            List<Table> bookLendRecordSelectList = new List<Table>();
            foreach (BookLendRecord bookLendRecordRows in bookLendRecordList)
            {
                Table bookLendRecordSelect = new Table();

                bookLendRecordSelect.KEEPER_ID = bookLendRecordRows.KEEPER_ID;
                bookLendRecordSelect.LEND_DATE = bookLendRecordRows.LEND_DATE;
                foreach (MemberM memberMRows in memberMList)
                {
                    if (bookLendRecordSelect.KEEPER_ID.Equals(memberMRows.USER_ID, StringComparison.CurrentCulture))
                    {
                        bookLendRecordSelect.USER_CNAME = memberMRows.USER_CNAME;
                        bookLendRecordSelect.USER_ENAME = memberMRows.USER_ENAME;
                    }
                }
                bookLendRecordSelectList.Add(bookLendRecordSelect);
            }

            return bookLendRecordSelectList;
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
    }
}