using System;
using System.Collections.Generic;

namespace bookMaintain.Model.Models
{
    public partial class Country
    {
        public int Id { get; set; }
        public string EnglishName { get; set; } = null!;
        public string ChineseName { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
