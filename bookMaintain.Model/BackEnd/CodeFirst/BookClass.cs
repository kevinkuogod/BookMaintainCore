using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookMaintain.Model.BackEnd.CodeFirst
{
    [Table("BOOK_CLASS")]
    /// <summary>
    /// BookData to BookClass一對多的部分
    /// </summary>
    public class BookClass
    {
        /// <summary>
        /// 類別代號
        /// </summary>
        ///[MaxLength(5)]
        [Required(ErrorMessage = "此欄位必填")]
        [DisplayName("類別代號")]
        [Key]
        public string BOOK_CLASS_ID { get; set; }

        /// <summary>
        /// 類別名稱
        /// </summary>
        [DisplayName("類別名稱")]
        public string BOOK_CLASS_NAME { get; set; }

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

        public virtual BookData BookData { get; set; }
    }
}
