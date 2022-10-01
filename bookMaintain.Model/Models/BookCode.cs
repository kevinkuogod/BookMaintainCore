using System;
using System.Collections.Generic;

namespace bookMaintain.Model.Models
{
    public partial class BookCode
    {
        public string CodeType { get; set; } = null!;
        public string CodeId { get; set; } = null!;
        public string? CodeTypeDesc { get; set; }
        public string? CodeName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyUser { get; set; }
    }
}
