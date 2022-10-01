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
    }
}