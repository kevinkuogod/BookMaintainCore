using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Linq;
using bookMaintain.Model.BackEnd.Arg.Vocabulary;
using bookMaintain.Model.BackEnd.Table.Vocabulary;
using System.Collections;
//using System.Runtime.Remoting.Messaging;
using bookMaintain.Common;
using System.Data.Entity.Core.Objects;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace bookMaintain.Dao.Ado
{
    public class VocabularyDao : IVocabularyDao
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

        /*public void Update<T>(T t) where T : Table
        {
            Type type = typeof(T);
            var propArray = type.GetProperties().Where(p=>!p.Name.Equals("ID"));
            string columnString = string.Join(",", propArray.Select(p => $"[{p.GetColumnName()}]=@{p.GetColumnName()}"));
            var parameters = propArray.Select(p => new SqlParameter($"@{p.GetColumnName()}", p.GetValue(t) ?? DBNull.Value)).ToArray();
            string sql = $"UPDATE {type.Name} SET {columnString} WHERE Id = { t.ID }";

            using (SqlConnection conn =  new SqlConnection(this.GetDBConnectionString()))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(parameters);
                conn.Open();
                int iResult = command.ExecuteNonQuery();
                if (iResult == 0)
                    throw new Exception("Update數據不存在");
            }
        }*/

        /// <summary>
        /// 約束是為了更正確的調用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        //public List<Table> Find<T>(string englishName) where T: Table
        /*public T Find<T>(string englishName) where T : Table
        {
            Type type = typeof(T);
            //string columnString = string.Join(",",type.GetProperties().Select(p=>$"[{p.Name}]"));
            string columnString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetColumnName()}]"));
            string vocabularyChancesSql = $"SELECT {columnString} FROM [{type.Name}] WHERE English_Name={englishName}";
            T t = (T)Activator.CreateInstance(type);
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString())) {
                SqlCommand command = new SqlCommand(vocabularyChancesSql, conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<T> list = this.ReaderToList<T>(reader);
                t = list.FirstOrDefault();
                //if (reader.Read())
                //{
                //    foreach(var prop in type.GetProperties())
                //    {
                //        prop.SetValue(t, reader[prop.Name] is DBNull? "": reader[prop.Name]);
                //    }
                //}
            }
            return t;
        }*/

        /*public List<T> FindAll<T>(string englishName) where T : Table
        {
            Type type = typeof(T);
            string columnString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetColumnName()}]"));
            string vocabularyChancesSql = $"SELECT {columnString} FROM [{type.Name}] WHERE English_Name={englishName}";
            List<T> list = new List<T>();
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                SqlCommand command = new SqlCommand(vocabularyChancesSql, conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                list = this.ReaderToList<T>(reader);
            }
            return list;
        }*/

        /// <summary>
        /// 一次應綁定輸入
        /// 一次應綁定輸入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        /*private List<T> ReaderToList<T>(SqlDataReader reader) where T : Table
        {
            Type type = typeof(T);
            List<T> list = new List<T>();
            while (reader.Read())
            {
                T t = (T)Activator.CreateInstance(type);
                foreach (var prop in type.GetProperties())
                {
                    object oValue = reader[prop.GetColumnName()];
                    if (oValue is DBNull)
                        oValue = null;
                    prop.SetValue(t, oValue);
                    //prop.SetValue(t, reader[prop.GetColumnName()] is DBNull ? "" : reader[prop.Name]);
                }
                list.Add(t);
            }
            return list;
        }*/

        public List<Table> GetTable(InsertArg arg)
        {
            return new List<Table>();
        }

            /// <summary>
            /// 輸入單字
            /// </summary>
            /// <returns></returns>
            //
            public int InsertVocabulary(InsertArg insertArg)
            {

            FileInfo fi = new FileInfo(insertArg.English_File.FileName);
            string path = "C:\\Users\\kevin\\Desktop\\" + fi.Name;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                //await insertArg.English_File.CopyToAsync(fileStream);
                insertArg.English_File.CopyTo(fileStream);
            }

            string vocabularyChancesSql = @"SELECT 
                                      Chances,
                                      Example_Sentences,
                                      Example_Sentences_Translation
                                      FROM Vocabulary
                                      WHERE English_Name = @English_Name";

            int vocabularyId = 0;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                DataTable searchDt = new DataTable();
                SqlTransaction transaction;
                transaction = conn.BeginTransaction();

                try
                { 
                    SqlCommand vocabularyChancesCmd = new SqlCommand(vocabularyChancesSql, conn);
                    //選出機率
                    int vocabularyChances;
                    vocabularyChancesCmd.Transaction = transaction;
                    vocabularyChancesCmd.Parameters.Add(new SqlParameter("@English_Name", insertArg.English_Name));
                    SqlDataAdapter vocabularyChancesCmdAdapter = new SqlDataAdapter(vocabularyChancesCmd);
                    vocabularyChancesCmdAdapter.Fill(searchDt);
                    int count = searchDt.Rows.Count;
                    if (count > 0)
                    {
                        vocabularyChances = ((int)searchDt.Rows[0][0])+1;
                        string Example_Sentences = "";
                        string Example_Sentences_Translation = "";
                        if (vocabularyChances > 1)
                        {
                            Example_Sentences = searchDt.Rows[0][1].ToString();
                            Example_Sentences_Translation = searchDt.Rows[0][2].ToString();
                        }
                        string updateVocabulary = @"UPDATE Vocabulary
                                                   SET
                                                       Example_Sentences             = @Example_Sentences,
                                                       Example_Sentences_Translation = @Example_Sentences_Translation,
                                                       Chances                       = @Chances
                                                  WHERE English_Name = @English_Name";
                        SqlCommand insertCmd = new SqlCommand(updateVocabulary, conn);
                        insertCmd.Transaction = transaction;
                        //可以判定有沒有句號
                        insertCmd.Parameters.Add(new SqlParameter("@Example_Sentences", Example_Sentences+insertArg.Example_Sentences));
                        insertCmd.Parameters.Add(new SqlParameter("@Example_Sentences_Translation", Example_Sentences_Translation+insertArg.Example_Sentences_Translation));
                        insertCmd.Parameters.Add(new SqlParameter("@Chances", vocabularyChances));
                        insertCmd.Parameters.Add(new SqlParameter("@English_Name", insertArg.English_Name));
                        insertCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        vocabularyChances = 1;
                        string insertVocabulary = @"INSERT INTO Vocabulary
        				 (
        					 English_Name, Chinese_Name, Language, Image_Id,
                             Part_Of_Speech, Part_Of_Speech_Detial,Example_Sentences,Example_Sentences_Translation,
                             Chances,Provenance,Editor,Kenyon_And_Knott,Professional_Field,Extra_Matters,
                             Tense,Remark,Perfect_Tense,Created_At,Updated_At
        				 )
        				VALUES
        				(
        					 @English_Name, @Chinese_Name,0, 0,
                             @Part_Of_Speech, @Part_Of_Speech_Detial,@Example_Sentences,@Example_Sentences_Translation,
                             @Chances,@Provenance,@Editor,@Kenyon_And_Knott,@Professional_Field,@Extra_Matters,
                             @Tense,@Remark,@Perfect_Tense,@Created_At,@Updated_At
        				)
                        SELECT SCOPE_IDENTITY()";
                        SqlCommand insertCmd = new SqlCommand(insertVocabulary, conn);
                        insertCmd.Transaction = transaction;
                        insertCmd.Parameters.Add(new SqlParameter("@English_Name", insertArg.English_Name));
                        insertCmd.Parameters.Add(new SqlParameter("@Chinese_Name", insertArg.Chinese_Name));
                        insertCmd.Parameters.Add(new SqlParameter("@Part_Of_Speech", insertArg.Part_Of_Speech));
                        insertCmd.Parameters.Add(new SqlParameter("@Part_Of_Speech_Detial", insertArg.Part_Of_Speech_Detial));
                        insertCmd.Parameters.Add(new SqlParameter("@Example_Sentences", insertArg.Example_Sentences));
                        insertCmd.Parameters.Add(new SqlParameter("@Example_Sentences_Translation", insertArg.Example_Sentences_Translation));
                        insertCmd.Parameters.Add(new SqlParameter("@Chances", vocabularyChances));
                        insertCmd.Parameters.Add(new SqlParameter("@Provenance", insertArg.Provenance));
                        insertCmd.Parameters.Add(new SqlParameter("@Editor", insertArg.Editor));
                        insertCmd.Parameters.Add(new SqlParameter("@Kenyon_And_Knott", insertArg.Kenyon_And_Knott));
                        insertCmd.Parameters.Add(new SqlParameter("@Professional_Field", insertArg.Professional_Field));
                        insertCmd.Parameters.Add(new SqlParameter("@Extra_Matters", insertArg.Extra_Matters));
                        insertCmd.Parameters.Add(new SqlParameter("@Tense", insertArg.Tense));
                        insertCmd.Parameters.Add(new SqlParameter("@Remark", insertArg.Remark));
                        insertCmd.Parameters.Add(new SqlParameter("@Perfect_Tense", insertArg.Perfect_Tense));
                        insertCmd.Parameters.Add(new SqlParameter("@Created_At", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")));
                        insertCmd.Parameters.Add(new SqlParameter("@Updated_At", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")));
                        vocabularyId = Convert.ToInt32(insertCmd.ExecuteScalar());
                    }
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
            return vocabularyId;
        }
    }
}