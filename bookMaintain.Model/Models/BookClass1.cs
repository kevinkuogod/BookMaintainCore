using System;
using System.Collections.Generic;

namespace bookMaintain.Model.Models
{
    public partial class BookClass1
    {
        public string BookClassId { get; set; } = null!;
        public string? BookClassName { get; set; }
        public string? CreateDate { get; set; }
        public string? CreateUser { get; set; }
        public string? ModifyDate { get; set; }
        public string? ModifyUser { get; set; }
        public int? BookDataBookId { get; set; }

        public virtual BookData? BookDataBook { get; set; }
    }
}
