using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bookMaintain.Model.BackEnd.CodeFirst
{
    /// <summary>
    /// BookData to BookClass 一對多的部分，利用BOOK_CLASS_ID
    /// 我們將從使用下列「一般舊 CLR 物件」 (POC) 開始， EF 提供數個 POCO 範本。
    /// </summary>
    public class BookData2
    {
        public BookData2(int bookID, string bookName, string bookClassId, string bookClassName)
        {
            BOOK_ID = bookID;
            BOOK_NAME = bookName;
            BOOK_CLASS_ID = bookClassId;
            BOOK_CLASS_NAME = bookClassName;
        }
        public int BOOK_ID { get; set; }
        /// <summary>
        /// 類別名稱
        /// </summary>
        ///[ForeignKey("AuthorId")]試加
        ///[InverseProperty(nameof(Like.Post))]
        ///[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("書籍名稱")]
        public string BOOK_NAME { get; set; }

        /// <summary>
        /// 類別代號BOOK_CLASS.BOOK_CLASS_ID
        /// </summary>
        [DisplayName("類別代號BOOK_CLASS.BOOK_CLASS_ID")]
        public string BOOK_CLASS_ID { get; set; }
        public string BOOK_CLASS_NAME { get; set; }
    }
}
