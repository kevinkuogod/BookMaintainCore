using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;
using bookMaintain.Model.BackEnd.Arg.BookMaintain;
using System.Net.Mail;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace bookMaintain.Dao.BackEnd.Ado
{
    public class LoginDao : ILoginDao
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
        /// 
        /// </summary>
        /// <returns>輸入圖書維護:大於0正確</returns>
        //
        public int Insert(Login insertArg)
        {
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                string sql = @"SELECT PASSWORD,Salt FROM CUSTOMERS WHERE EMAIL = @EMAIL";
                DataTable searchDt = new DataTable();
                SqlCommand vocabularyChancesCmd = new SqlCommand(sql, conn);
                vocabularyChancesCmd.Parameters.Add(new SqlParameter("@EMAIL", insertArg.EMAIL));
                SqlDataAdapter vocabularyChancesCmdAdapter = new SqlDataAdapter(vocabularyChancesCmd);
                vocabularyChancesCmdAdapter.Fill(searchDt);
                int count = searchDt.Rows.Count;
                if (count > 0)
                {
                    string test = searchDt.Rows[0][1].ToString();
                    string Password = "";
                    string KeyContainerName = null;
                    System.Security.Cryptography.CspParameters param = new System.Security.Cryptography.CspParameters();
                    param.KeyContainerName = KeyContainerName ?? searchDt.Rows[0][1].ToString(); //密匙容器的名称，保持加密解密一致才能解密成功
                    using (System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(param))
                    {
                        byte[] encryptdata = Convert.FromBase64String(searchDt.Rows[0][0].ToString());
                        byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                        Password = System.Text.Encoding.Default.GetString(decryptdata);
                    }

                    if (insertArg.PASSWORD.Equals(Password, StringComparison.CurrentCulture))
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
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
                    byte[] plaindata = System.Text.Encoding.Default.GetBytes(forgetPasswordArg.PASSWORD);//将要加密的字符串转换为字节数组
                    byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
                    forgetPasswordArg.PASSWORD = Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串
                }

                try
                {
                    cmd.Transaction = transaction;
                    cmd.Parameters.Add(new SqlParameter("@EMAIL", forgetPasswordArg.EMAIL));
                    cmd.Parameters.Add(new SqlParameter("@Salt", salt));
                    cmd.Parameters.Add(new SqlParameter("@PASSWORD", forgetPasswordArg.PASSWORD));
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