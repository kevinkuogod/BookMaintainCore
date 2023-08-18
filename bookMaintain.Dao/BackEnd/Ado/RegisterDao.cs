using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;
using bookMaintain.Model.BackEnd.Arg.BookMaintain;
using System.Net.Mail;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Security.Cryptography.Xml;

namespace bookMaintain.Dao.BackEnd.Ado
{
    public class RegisterDao : IRegisterDao
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
        public int Insert(Register insertArg)
        {
            /*try
            {
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.To.Add(insertArg.EMAIL);
                //msg.To.Add("b@b.com");可以發送給多人
                //msg.CC.Add("c@c.com");
                //msg.CC.Add("c@c.com");可以抄送副本給多人 
                //這裡可以隨便填，不是很重要
                msg.From = new MailAddress("kevin@tko.idv.tw", "大魚", System.Text.Encoding.UTF8);
            */
            /* 上面3個參數分別是發件人地址（可以隨便寫），發件人姓名，編碼*/
            /*    msg.Subject = "測試標題";//郵件標題
                msg.SubjectEncoding = System.Text.Encoding.UTF8;//郵件標題編碼
                msg.Body = "<b>測試一下</b>"; //郵件內容
                msg.BodyEncoding = System.Text.Encoding.UTF8;//郵件內容編碼 
                msg.Attachments.Add(new Attachment(@"C:\Users\kevin\Desktop\下載.jpg"));  //附件
                msg.IsBodyHtml = true;//是否是HTML郵件 
                                      //msg.Priority = MailPriority.High;//郵件優先級 

                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("kevin@tko.idv.tw", ""); //這裡要填正確的帳號跟密碼
                client.Host = "smtp.gmail.com"; //設定smtp Server
                client.Port = 25; //設定Port
                client.EnableSsl = true; //gmail預設開啟驗證
                client.Send(msg); //寄出信件
                client.Dispose();
                msg.Dispose();
                //MessageBox.Show(this, "郵件寄送成功！");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.Message);
            }*/

            string sql = @"INSERT INTO CUSTOMERS
        				 (
        					 FIRST_NAME, LAST_NAME, 
                             ROLE,EMAIL, EMAIL_VERIFIED_AT, 
                             TELEPHONE,FAX,PASSWORD,Salt,
                             CREATE_DATE, CREATE_USER, MODIFY_DATE, MODIFY_USER
                             ,REMEMBER_TOKEN
        				 )
        				VALUES
        				(
        					 @FIRST_NAME, @LAST_NAME, 
                             '2',@EMAIL, @EMAIL_VERIFIED_AT,
                            @TELEPHONE,@FAX, @PASSWORD,@Salt
                            , @CREATE_DATE, 'admin', @MODIFY_DATE, 'admin'
                            ,'159',@PublicKey,@PrivateKey
        				)
                        SELECT SCOPE_IDENTITY()";

            int RegisterId = 0;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlTransaction transaction;
                transaction = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand(sql, conn);

                //加密密碼
                /*
                    byte[] buffer;
                    DESCryptoServiceProvider DesCSP = new DESCryptoServiceProvider();
                    MemoryStream ms = new MemoryStream();//先建立 一個記憶體流
                    CryptoStream cryStream = new CryptoStream(ms, DesCSP.CreateEncryptor(), CryptoStreamMode.Write);//將記憶體流連線到加密轉換流
                    StreamWriter sw = new StreamWriter(cryStream);
                    sw.WriteLine(insertArg.PASSWORD);//將要加密的字串寫入加密轉換流
                    sw.Close();
                    cryStream.Close();
                    buffer = ms.ToArray();//將加密後的流轉換為位元組陣列
                    insertArg.PASSWORD = Convert.ToBase64String(buffer);//將加密後的位元組陣列轉換為字串
                */


                string salt = bookMaintain.Common.CreateRandom.createRandomString();
                /*IIS 無法使用所以改方法
                string KeyContainerName = null;
                System.Security.Cryptography.CspParameters param = new System.Security.Cryptography.CspParameters();
                param.KeyContainerName = KeyContainerName ?? salt; //密匙容器的名称，保持加密解密一致才能解密成功
                using (System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(param))
                {
                    byte[] plaindata = System.Text.Encoding.Default.GetBytes(insertArg.PASSWORD);//将要加密的字符串转换为字节数组
                    byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
                    insertArg.PASSWORD = Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串
                }*/

                /*RSAEncryptionPadding padding;
                using (RSA rsa = RSA.Create())
                {
                    padding = RSAEncryptionPadding.OaepSHA256;
                    byte[] encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(insertArg.PASSWORD), padding);
                    insertArg.PASSWORD = Convert.ToBase64String(encryptedData);//将加密后的字节数组转换为字符串
                }*/

                Tuple<string, string> ksys = bookMaintain.Common.RSA.GenerateRSAKeys();
                insertArg.PASSWORD = bookMaintain.Common.RSA.Encrypt(ksys.Item1, insertArg.PASSWORD, salt);

                try
                {
                    cmd.Transaction = transaction;
                    //cmd.Parameters.Add(new SqlParameter("@ID", insertArg.ID));
                    cmd.Parameters.Add(new SqlParameter("@FIRST_NAME", insertArg.FIRST_NAME));
                    cmd.Parameters.Add(new SqlParameter("@LAST_NAME", insertArg.LAST_NAME));
                    cmd.Parameters.Add(new SqlParameter("@EMAIL", insertArg.EMAIL));
                    cmd.Parameters.Add(new SqlParameter("@EMAIL_VERIFIED_AT", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")));
                    cmd.Parameters.Add(new SqlParameter("@TELEPHONE", insertArg.TELEPHONE));
                    cmd.Parameters.Add(new SqlParameter("@FAX", insertArg.FAX));
                    cmd.Parameters.Add(new SqlParameter("@Salt", salt));
                    cmd.Parameters.Add(new SqlParameter("@PASSWORD", insertArg.PASSWORD));
                    cmd.Parameters.Add(new SqlParameter("@PublicKey", ksys.Item1));
                    cmd.Parameters.Add(new SqlParameter("@PrivateKey", ksys.Item2));
                    cmd.Parameters.Add(new SqlParameter("@CREATE_DATE", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")));
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