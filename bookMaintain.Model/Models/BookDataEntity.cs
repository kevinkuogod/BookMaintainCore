using System;
using System.Collections.Generic;

namespace bookMaintain.Model.Models
{
    public partial class BookDataEntity
    {
        public BookDataEntity()
        {
            BookClassEntities = new HashSet<BookClassEntity>();
        }

        public int BookId { get; set; }
        public string? BookName { get; set; }
        public string? BookClassId { get; set; }
        public string? BookAuthor { get; set; }
        public string? BookBoughtDate { get; set; }
        public string? BookPublisher { get; set; }
        public string? BookNote { get; set; }
        public string? BookStatus { get; set; }
        public string? BookKeeper { get; set; }
        public int BookAmount { get; set; }
        public string? CreateDate { get; set; }
        public string? CreateUser { get; set; }
        public string? ModifyDate { get; set; }
        public string? ModifyUser { get; set; }

        public virtual ICollection<BookClassEntity> BookClassEntities { get; set; }
    }
}
