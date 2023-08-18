using System.ComponentModel;

namespace bookMaintain.Model.BackEnd.Arg.BookMaintain
{
    //[ID][bigint] NOT NULL,                 /*顧客ID*/
    //[FIRSTNAME] [nvarchar] (50) NOT NULL,	/*顧客'姓'後名稱*/
    //[LASTNAME] [nvarchar] (50) NOT NULL,	/*顧客'姓'名稱*/
    //[ROLE] [int] NULL,			/*角色*/
    //[EMAIL][nvarchar] (250) NOT NULL,       /*信箱*/
    //[EMAIL_VERIFIED_AT] [datetime] NULL,	/*信箱驗證時間*/
    //[TELEPHONE][nvarchar] (50) NULL,	/*電話名稱*/
    //[FAX][nvarchar] (50) NULL,		/*傳真*/
    //[PASSWORD][nvarchar] (255) NOT NULL,	/*密碼*/
    //[REMEMBER_TOKEN] [nvarchar] (100) NOT NULL,	/*備註*/
    //[CREATE_DATE] [datetime] NULL,	/*創建時間*/
    //[CREATE_USER][nvarchar] (50) NULL, /*創建者*/
    //[MODIFY_DATE][datetime] NULL,	/*更改時間*/
    //[MODIFY_USER][nvarchar] (50) NULL, /*更改使用者*/

    //書籍資料檔				
    public class Register2
    {
        /// <summary>
        /// 顧客ID
        /// </summary>
        [DisplayName("顧客ID")]
        public int ID { get; set; }

        /// <summary>
        /// 顧客'姓'後名稱
        /// </summary>
        [DisplayName("顧客姓後名稱")]
        public string? FIRST_NAME { get; set; }

        /// <summary>
        /// 顧客'姓'名稱
        /// </summary>
        [DisplayName("顧客姓名稱")]
        public string? LAST_NAME { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [DisplayName("角色")]
        public string? ROLE { get; set; }

        /// <summary>
        /// 信箱
        /// </summary>
        [DisplayName("信箱")]
        public string? EMAIL { get; set; }

        /// <summary>
        /// 信箱驗證時間
        /// </summary>
        [DisplayName("信箱驗證時間")]
        public string? EMAIL_VERIFIED_AT { get; set; }

        /// <summary>
        /// 電話名稱
        /// </summary>
        [DisplayName("電話名稱")]
        public string? TELEPHONE { get; set; }

        /// <summary>
        /// 傳真
        /// </summary>
        [DisplayName("傳真")]
        public string? FAX { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [DisplayName("密碼")]
        public int PASSWORD { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [DisplayName("備註")]
        public string? REMEMBER_TOKEN { get; set; }

        /// <summary>
        /// 創建時間
        /// </summary>
        [DisplayName("創建時間")]
        public string? CREATE_DATE { get; set; }

        /// <summary>
        /// 創建者
        /// </summary>
        [DisplayName("創建者")]
        public string? CREATE_USER { get; set; }

        /// <summary>
        /// 更改時間
        /// </summary>
        [DisplayName("更改時間")]
        public string? MODIFY_DATE { get; set; }

        /// <summary>
        /// 更改使用者
        /// </summary>
        [DisplayName("更改使用者")]
        public int MODIFY_USER { get; set; }
    }
}
