using System;
using System.Collections.Generic;

namespace bookMaintain.Model.Models
{
    public partial class BookClass
    {
        public string BookClassId { get; set; } = null!;
        public string BookClassName { get; set; } = null!;
        public DateTime? CreateDate { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyUser { get; set; }
    }
}
