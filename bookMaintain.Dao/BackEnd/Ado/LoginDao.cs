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
using System.Dynamic;
using System.Reflection.Metadata;
using bookMaintain.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using SqlConnection = Microsoft.Data.SqlClient.SqlConnection;
using SqlCommand = Microsoft.Data.SqlClient.SqlCommand;
using SqlParameter = Microsoft.Data.SqlClient.SqlParameter;
using SqlDataAdapter = Microsoft.Data.SqlClient.SqlDataAdapter;
using log4net;
using static bookMaintain.Dao.BackEnd.Ado.LoginDao;
using SqlTransaction = Microsoft.Data.SqlClient.SqlTransaction;

namespace bookMaintain.Dao.BackEnd.Ado
{
    public class LoginDao : ILoginDao
    {
        /// <summary>
        /// 取得DB連線字串
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString(string connString= "ConnectionStrings:Default")
        {
            return bookMaintain.Common.ConfigTool.GetDBConnectionString(connString);
        }

        //用於List model表
        //public record LoginMessage(string LAST_NAME, string FIRST_NAME, string PASSWORD, string Salt, string PrivateKey);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>輸入圖書維護:大於0正確</returns>
        //
        public dynamic InsertMysql(Login insertArg)
        {
            string sql = @"SELECT LAST_NAME, FIRST_NAME, PASSWORD, Salt, PrivateKey FROM CUSTOMERS WHERE EMAIL = @EMAIL";
            //可裝dapper
            DataTable mysqldt = new DataTable();
            mysqldt.Columns.Add(new DataColumn("LAST_NAME", typeof(string)));
            mysqldt.Columns.Add(new DataColumn("FIRST_NAME", typeof(string)));
            mysqldt.Columns.Add(new DataColumn("PASSWORD", typeof(string)));
            mysqldt.Columns.Add(new DataColumn("Salt", typeof(string)));
            mysqldt.Columns.Add(new DataColumn("PrivateKey", typeof(string)));
            MySqlConnection conn3 = new MySqlConnection();
            conn3.ConnectionString = this.GetDBConnectionString("ConnectionStrings:DefaultMysql");
            if (conn3.State != ConnectionState.Open)
                {
                    conn3.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn3);
                    cmd.Parameters.Add(new MySqlParameter("@EMAIL", insertArg.Email));
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    //Reader.HasRows
                    if (Reader.Read())
                    {
                        DataRow dataRow = mysqldt.NewRow();
                        dataRow["LAST_NAME"] = Reader.GetString(0);
                        dataRow["FIRST_NAME"] = Reader.GetString(1);
                        dataRow["PASSWORD"] = Reader.GetString(2);
                        dataRow["Salt"] = Reader.GetString(3);
                        dataRow["PrivateKey"] = Reader.GetString(4);
                        mysqldt.Rows.Add(dataRow);
                    }
                    conn3.Close();
            }
            dynamic loginObject = new ExpandoObject();
            loginObject.loginStatus = 0;
            if (mysqldt.Rows.Count > 0)
            {
                DataRow loginResult = mysqldt.Rows[0];

                string Password = "";
                string KeyContainerName = null;
                string decryptdatastring = bookMaintain.Common.RSA.Decrypt(loginResult["PrivateKey"].ToString(), loginResult["PASSWORD"].ToString());
                Logger.Write(Logger.LogCategoryEnum.Error, "1.14" + decryptdatastring);
                //1234567gpvYVv8r
                if ((insertArg.Password + loginResult["Salt"].ToString()).Equals(decryptdatastring, StringComparison.CurrentCulture))
                {
                    loginObject.loginStatus = 1;
                    loginObject.loginName = loginResult["LAST_NAME"].ToString() + loginResult["FIRST_NAME"].ToString();
                    return loginObject;
                }
                else
                {
                    return loginObject;
                }
            }
            else
            {
                return loginObject;
            }
        }
        public dynamic Insert(Login insertArg)
        {
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                string sql = @"SELECT LAST_NAME, FIRST_NAME, PASSWORD, Salt, PrivateKey FROM CUSTOMERS WHERE EMAIL = @EMAIL";
                DataTable searchDt = new DataTable();
                SqlCommand vocabularyChancesCmd = new SqlCommand(sql, conn);
                vocabularyChancesCmd.Parameters.Add(new SqlParameter("@EMAIL", insertArg.Email));
                SqlDataAdapter vocabularyChancesCmdAdapter = new SqlDataAdapter(vocabularyChancesCmd);
                vocabularyChancesCmdAdapter.Fill(searchDt);
                int count = searchDt.Rows.Count;
                conn.Close();

                /*if (count > 0)
                {
                    DataRow row = searchDt.AsEnumerable().FirstOrDefault();
                    if (row?.Table.Columns.Count == 0)
                    {

                    }
                    row.Field<string>("PASSWORD");
                }*/
                dynamic loginObject = new ExpandoObject();
                loginObject.loginStatus = 0;
                if (count > 0)
                {
                    DataRow loginResult = searchDt.Rows[0];

                    string Password = "";
                    string KeyContainerName = null;

                    /*using (RSA rsa = RSA.Create())
                    {
                        RSAEncryptionPadding padding = RSAEncryptionPadding.OaepSHA256;
                        Logger.Write(Logger.LogCategoryEnum.Error, "1.11" + padding.ToString());
                        byte[] encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(loginResult["PASSWORD"].ToString()), padding);
                        byte[] decryptedData = rsa.Decrypt(encryptedData, padding);
                        string decryptedText = Encoding.UTF8.GetString(decryptedData);
                        Logger.Write(Logger.LogCategoryEnum.Error, "1.12" + decryptedText);
                    }*/
                    //不寫stroe procedure的原因要解密用私密金鑰
                    //有壞的帳戶可以開，刷key
                    //Tuple<string, string> ksys = GenerateRSAKeys();
                    //string encryptdatastring = Encrypt(ksys.Item1, insertArg.Password, loginResult["Salt"].ToString());
                    string decryptdatastring = bookMaintain.Common.RSA.Decrypt(loginResult["PrivateKey"].ToString(), loginResult["PASSWORD"].ToString());
                    /*Logger.Write(Logger.LogCategoryEnum.Error, "1.12" + encryptdatastring);
                    Logger.Write(Logger.LogCategoryEnum.Error, "1.13" + ksys.Item1);
                    Logger.Write(Logger.LogCategoryEnum.Error, "1.14" + ksys.Item2);*/
                    Logger.Write(Logger.LogCategoryEnum.Error, "1.14" + decryptdatastring);
                    //1234567gpvYVv8r
                    if ((insertArg.Password+ loginResult["Salt"].ToString()).Equals(decryptdatastring, StringComparison.CurrentCulture))
                    {
                        /*string setStatusSql = "";
                        if (insertArg.MachineType.Equals("PhoneLoginState", StringComparison.CurrentCulture))
                        {
                            setStatusSql = @"UPDATE CUSTOMERS SET PhoneLoginState=1 WHERE EMAIL=@EMAIL";
                        }
                        else if(insertArg.MachineType.Equals("WebLoginState", StringComparison.CurrentCulture))
                        {
                            setStatusSql = @"UPDATE CUSTOMERS SET WebLoginState=1 WHERE EMAIL=@EMAIL";
                        }
                        using (SqlConnection conn2 = new SqlConnection(this.GetDBConnectionString()))
                        {
                            conn2.Open();
                            SqlTransaction transaction;
                            transaction = conn2.BeginTransaction();
                            SqlCommand cmd = new SqlCommand(setStatusSql, conn2);
                            //int RegisterId = 0;
                            try
                            {
                                cmd.Transaction = transaction;
                                cmd.Parameters.Add(new SqlParameter("@EMAIL", insertArg.Email));                                
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                                conn2.Close();
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
                                }
                                bookMaintain.Common.Logger.Write(bookMaintain.Common.Logger.LogCategoryEnum.Error, parameterEx.ToString());
                            }
                        }*/
                        loginObject.loginStatus = 1;
                        loginObject.loginName = loginResult["LAST_NAME"].ToString() + loginResult["FIRST_NAME"].ToString();
                        return loginObject;
                    }
                    else
                    {
                        return loginObject;
                    }

                    /*IIS無法使用，而且錯誤的理解方式解密，沒使用公司對
                     * Logger.Write(Logger.LogCategoryEnum.Error, "2." + loginResult["PASSWORD"].ToString());
                    System.Security.Cryptography.CspParameters param = new System.Security.Cryptography.CspParameters();
                    param.KeyContainerName = KeyContainerName ?? loginResult["Salt"].ToString(); //密匙容器的名称，保持加密解密一致才能解密成功
                    Logger.Write(Logger.LogCategoryEnum.Error, "2.1" + param.KeyContainerName);
                    using (System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(param))
                    {
                        Logger.Write(Logger.LogCategoryEnum.Error, "2.2");
                        byte[] encryptdata = Convert.FromBase64String(loginResult["PASSWORD"].ToString());
                        Logger.Write(Logger.LogCategoryEnum.Error, "2.3"+ encryptdata.ToString());
                        byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                        Logger.Write(Logger.LogCategoryEnum.Error, "2.4" + decryptdata.ToString());
                        Password = System.Text.Encoding.Default.GetString(decryptdata);
                        Logger.Write(Logger.LogCategoryEnum.Error, "3." + Password);
                    }
                    Logger.Write(Logger.LogCategoryEnum.Error, "4." + insertArg.Password);
                    if (insertArg.Password.Equals(Password, StringComparison.CurrentCulture))
                    {
                        loginObject.loginStatus = 1;
                        loginObject.loginName = loginResult["LAST_NAME"].ToString() + loginResult["FIRST_NAME"].ToString();
                        return loginObject;
                    }
                    else
                    {
                        return loginObject;
                    }*/
                }
                else
                {
                    return loginObject;
                }
            }
        }

        public int ForgetPassword(Login forgetPasswordArg)
        {
            string sql = @"UPDATE CUSTOMERS
                           SET
                            MODIFY_DATE=@MODIFY_DATE, MODIFY_USER='admin',PASSWORD=@PASSWORD,Salt=@Salt
                           WHERE
                                EMAIL=@EMAIL";

            int RegisterId = 0;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlTransaction transaction;
                transaction = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand(sql, conn);

                string salt = bookMaintain.Common.CreateRandom.createRandomString();
                string KeyContainerName = null;
                System.Security.Cryptography.CspParameters param = new System.Security.Cryptography.CspParameters();
                param.KeyContainerName = KeyContainerName ?? salt; //密匙容器的名称，保持加密解密一致才能解密成功
                using (System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(param))
                {
                    byte[] plaindata = System.Text.Encoding.Default.GetBytes(forgetPasswordArg.Password);//将要加密的字符串转换为字节数组
                    byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
                    forgetPasswordArg.Password = Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串
                }

                try
                {
                    cmd.Transaction = transaction;
                    cmd.Parameters.Add(new SqlParameter("@EMAIL", forgetPasswordArg.Email));
                    cmd.Parameters.Add(new SqlParameter("@Salt", salt));
                    cmd.Parameters.Add(new SqlParameter("@PASSWORD", forgetPasswordArg.Password));
                    cmd.Parameters.Add(new SqlParameter("@MODIFY_DATE", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")));
                    RegisterId = Convert.ToInt32(cmd.ExecuteScalar());
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
            return RegisterId;
        }
    }
}