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
using bookMaintain.Model.BackEnd.Table.Food;
using System.Linq;
using Azure.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MySql.Data.MySqlClient;
using bookMaintain.Model.BackEnd.Arg.Vocabulary;

namespace bookMaintain.Dao.BackEnd.Ado
{
    public class FoodDao : IFoodDao
    {
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return bookMaintain.Common.ConfigTool.GetDBConnectionString("ConnectionStrings:Food");
        }

        public async Task<int> CutFood(string conmment)
        {
            if (conmment == null)
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
                        SELECT SCOPE_IDENTITY()";
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

        public async Task<List<Food>> GetFood(/*SearchArg arg*/)
        {
            DataSet bookDs = new DataSet();

            string bookData = @"SELECT [ID]
                                    ,[ImgSrc]
                                    ,[Type]
                                    ,[Quantity]
                                    ,[Content]
                                    ,[CREATE_DATE]
                                    ,[CREATE_USER]
                                    ,[MODIFY_DATE]
                                    ,[MODIFY_USER]
                                    ,[Name]
                                    ,[Price]
                                FROM [FOOD].[dbo].[Item];";
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

                //(int)(long)
                //Convert.ToInt32(item.Field<int>("ID"))
                DataTable tables = bookDs.Tables["bookTable"];
                var bookDtListPre = (from item in tables.AsEnumerable()
                                     select new Food()
                                     {
                                         ID = item.Field<long>("ID"),
                                         ImgSrc = item.Field<string>("ImgSrc"),
                                         Type = item.Field<string>("Type"),
                                         Quantity = item.Field<int>("Quantity"),
                                         Content = item.Field<string>("Content"),
                                         CREATE_DATE = item.Field<DateTime>("CREATE_DATE"),
                                         CREATE_USER = item.Field<string>("CREATE_USER"),
                                         MODIFY_DATE = item.Field<DateTime>("MODIFY_DATE"),
                                         Price = item.Field<int>("Price"),
                                         Name = item.Field<string>("Name"),
                                         Number = 0
                                     }).ToList();

                return bookDtListPre;
            }
            catch (Exception ex)
            {
                return new List<Food>();
            }
        }

        private string GetDBConnectionString(string connString = "ConnectionStrings:Default")
        {
            return bookMaintain.Common.ConfigTool.GetDBConnectionString(connString);
        }

        public List<Food> GetFoodMysql(/*SearchArg arg*/)
        {
            string sql = @"SELECT ID,ImgSrc,Type,Quantity,Content,CREATE_DATE,CREATE_USER,MODIFY_DATE,MODIFY_USER,Name,Price FROM Item";
            DataTable mysqldt = new DataTable();
            mysqldt.Columns.Add(new DataColumn("ID", typeof(int)));
            mysqldt.Columns.Add(new DataColumn("ImgSrc", typeof(string)));
            mysqldt.Columns.Add(new DataColumn("Type", typeof(string)));
            mysqldt.Columns.Add(new DataColumn("Quantity", typeof(int)));
            mysqldt.Columns.Add(new DataColumn("Content", typeof(string)));
            mysqldt.Columns.Add(new DataColumn("CREATE_DATE", typeof(DateTime)));
            mysqldt.Columns.Add(new DataColumn("CREATE_USER", typeof(string)));
            mysqldt.Columns.Add(new DataColumn("MODIFY_DATE", typeof(DateTime)));
            mysqldt.Columns.Add(new DataColumn("MODIFY_USER", typeof(string)));
            mysqldt.Columns.Add(new DataColumn("Name", typeof(string)));
            mysqldt.Columns.Add(new DataColumn("Price", typeof(int)));
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = this.GetDBConnectionString("ConnectionStrings:DefaultMysql");
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    DataRow dataRow = mysqldt.NewRow();
                    dataRow["ID"] = Reader.GetString(0);
                    Console.WriteLine("ID:"+ Reader.GetString(0));
                    dataRow["ImgSrc"] = Reader.GetString(1);
                    Console.WriteLine("ImgSrc:" + Reader.GetString(1));
                    dataRow["Type"] = Reader.GetString(2);
                    Console.WriteLine("Type:" + Reader.GetString(2));
                    dataRow["Quantity"] = Reader.GetString(3);
                    Console.WriteLine("Quantity:" + Reader.GetString(3));
                    dataRow["Content"] = Reader.GetString(4);
                    Console.WriteLine("Content:" + Reader.GetString(4));
                    dataRow["CREATE_DATE"] = Reader.GetString(5);
                    Console.WriteLine("CREATE_DATE:" + Reader.GetString(5));
                    dataRow["CREATE_USER"] = Reader.GetString(6);
                    Console.WriteLine("CREATE_USER:" + Reader.GetString(6));
                    dataRow["MODIFY_DATE"] = Reader.GetString(7);
                    Console.WriteLine("MODIFY_DATE:" + Reader.GetString(7));
                    dataRow["MODIFY_USER"] = Reader.GetString(8);
                    Console.WriteLine("MODIFY_USER:" + Reader.GetString(8));
                    dataRow["Name"] = Reader.GetString(9);
                    Console.WriteLine("Name:" + Reader.GetString(9));
                    dataRow["Price"] = Reader.GetString(10);
                    Console.WriteLine("Price:" + Reader.GetString(10));
                    mysqldt.Rows.Add(dataRow);
                }
                conn.Close();
            }

            List<Food> listName = mysqldt.AsEnumerable().Select(m => new Food()
            {
                ID = m.Field<int>("ID"),
                ImgSrc = m.Field<string>("ImgSrc"),
                Type = m.Field<string>("Type"),
                Quantity = m.Field<int>("Quantity"),
                Content = m.Field<string>("Content"),
                CREATE_DATE = m.Field<DateTime>("CREATE_DATE"),
                CREATE_USER = m.Field<string>("CREATE_USER"),
                MODIFY_DATE = m.Field<DateTime>("MODIFY_DATE"),
                MODIFY_USER = m.Field<string>("MODIFY_USER"),
                Name = m.Field<string>("Name"),
                Price = m.Field<int>("Price"),
            }).ToList();

            return listName;
        }
    }
}