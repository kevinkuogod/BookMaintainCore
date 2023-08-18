using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;
using bookMaintain.Model.BackEnd.Table.BookLendRecord;
using bookMaintain.Model.BackEnd.Arg.BookLendRecord;
using bookMaintain.Model.BackEnd.CodeFirst;
using bookMaintain.Model.BackEnd.Arg.WebSocket;
using System.Data.SqlTypes;

namespace bookMaintain.Dao.BackEnd.Ado
{
    public class WebSocketDao : IWebSocketDao
    {
        /// <summary>
        /// 取得GetBookLendRecordDataByCondtioin的全部資料
        /// 換為GetTable
        /// </summary>
        /// <returns></returns>
        public async Task<int> InsertWebSocketContent(ChatroomContent content)
        {
            string sql = @"INSERT INTO WebSocketContent
        				 (Name, Content, CREATE_DATE)
        				VALUES(@Name, @Content,@CREATE_DATE)
                        SELECT SCOPE_IDENTITY()";
            int BookMaintainId = 0;
            using (SqlConnection conn = this.switchConnectDatabase())
            {
                conn.Open();
                SqlTransaction transaction;
                transaction = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand(sql, conn);
                try
                {
                    //room 
                    cmd.Transaction = transaction;
                    cmd.Parameters.Add(new SqlParameter("@Name", content.Name));
                    cmd.Parameters.Add(new SqlParameter("@Content", content.Content));
                    cmd.Parameters.Add(new SqlParameter("@CREATE_DATE", content.CREATE_DATE));
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

        public SqlConnection switchConnectDatabase()
        {
            SqlConnection conn = new SqlConnection(this.GetDBConnectionString("ConnectionStrings:Default"));
            /*int databaseCount = 0;
            List<string> databaseArray = new List<string>(){ "ConnectionStrings: DBConn1", "ConnectionStrings: DBConn2"};
            while ((conn.State != ConnectionState.Open) && (databaseCount<1))
            {
                conn = new SqlConnection(this.GetDBConnectionString(databaseArray[databaseCount]));
                databaseCount++;
            }*/
            return conn;
        }

        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString(string connString)
        {
            return bookMaintain.Common.ConfigTool.GetDBConnectionString(connString);
        }
    }
}