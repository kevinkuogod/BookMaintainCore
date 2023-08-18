using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bookMaintain.Model.BackEnd.EFCore
{
    /// <summary>
    /// BookData to BookClass 一對多的部分，利用BOOK_CLASS_ID
    /// 我們將從使用下列「一般舊 CLR 物件」 (POC) 開始， EF 提供數個 POCO 範本。
    /// </summary>
    public class BookData
    {
        #pragma warning disable CS8618
        public BookData()
        {
            this.BookClass = new HashSet<BookClass>();
        }

        /// <summary>
        /// 類別代號
        /// </summary>
        [Key]
        [DisplayName("類別代號")]
        public int BOOK_ID { get; set; }

        /// <summary>
        /// 類別名稱
        /// </summary>
        [DisplayName("書籍名稱")]
        public string BOOK_NAME { get; set; }

        /// <summary>
        /// 類別代號BOOK_CLASS.BOOK_CLASS_ID
        /// </summary>
        [DisplayName("類別代號BOOK_CLASS.BOOK_CLASS_ID")]
        public string BOOK_CLASS_ID { get; set; }

        /// <summary>
        /// 書籍作者
        /// </summary>
        [DisplayName("書籍作者")]
        public string BOOK_AUTHOR { get; set; }

        /// <summary>
        /// 書籍購書日期
        /// </summary>
        [DisplayName("書籍購書日期")]
        public string BOOK_BOUGHT_DATE { get; set; }

        /// <summary>
        /// 出版商
        /// </summary>
        [DisplayName("出版商")]
        public string BOOK_PUBLISHER { get; set; }

        /// <summary>
        /// 內容簡介
        /// </summary>
        [DisplayName("內容簡介")]
        public string BOOK_NOTE { get; set; }

        /// <summary>
        /// 狀態BOOK_CODE.CODE_ID (A可以借出 B以借出 U不可借出)
        /// </summary>
        [DisplayName("狀態(A可以借出 B以借出 U不可借出)")]
        public string BOOK_STATUS { get; set; }

        /// <summary>
        /// 書籍保管人
        /// </summary>
        [DisplayName("書籍保管人")]
        public string BOOK_KEEPER { get; set; }

        /// <summary>
        /// 書籍購買金額
        /// </summary>
        [DisplayName("書籍購買金額")]
        public int BOOK_AMOUNT { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [DisplayName("建立時間")]
        public string CREATE_DATE { get; set; }

        /// <summary>
        /// 建立使用者
        /// </summary>
        [DisplayName("建立使用者")]
        public string CREATE_USER { get; set; }

        /// <summary>
        /// 修改時間
        /// </summary>
        [DisplayName("修改時間")]
        public string MODIFY_DATE { get; set; }

        /// <summary>
        /// 修改使用者
        /// </summary>
        [DisplayName("修改使用者")]
        public string MODIFY_USER { get; set; }
        /// <summary>
        /// 跟Hset有關
        /// </summary>
        public virtual ICollection<BookClass> BookClass { get; set; }    
    }
    /*
    public ICollection<BookClass> BookClasses
    {
        get => LazyLoader.Load(this, ref _posts);
        set => _posts = value;
    }
    */
}
