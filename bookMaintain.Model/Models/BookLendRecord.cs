using System;
using System.Collections.Generic;

namespace bookMaintain.Model.Models
{
    public partial class BookLendRecord
    {
        public int IdentityFiled { get; set; }
        public int BookId { get; set; }
        public string KeeperId { get; set; } = null!;
        public DateTime LendDate { get; set; }
        public DateTime? CreDate { get; set; }
        public string? CreUsr { get; set; }
        public DateTime? ModDate { get; set; }
        public string? ModUsr { get; set; }
    }
}
