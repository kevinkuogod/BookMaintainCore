using System;
using System.Collections.Generic;

namespace bookMaintain.Model.Models
{
    public partial class SpanTable
    {
        public int IdentityFiled { get; set; }
        public string? SpanYear { get; set; }
        public string SpanStart { get; set; } = null!;
        public string SpanEnd { get; set; } = null!;
        public string? Note { get; set; }
        public DateTime? CreDate { get; set; }
        public string? CreUsr { get; set; }
        public DateTime? ModDate { get; set; }
        public string? ModUsr { get; set; }
    }
}
