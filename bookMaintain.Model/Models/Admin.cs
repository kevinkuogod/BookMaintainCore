using System;
using System.Collections.Generic;

namespace bookMaintain.Model.Models
{
    public partial class Admin
    {
        public long Id { get; set; }
        public string? UserCname { get; set; }
        public string? UserEname { get; set; }
        public int? Role { get; set; }
        public string Email { get; set; } = null!;
        public DateTime? EmailVerifiedAt { get; set; }
        public string Password { get; set; } = null!;
        public string RememberToken { get; set; } = null!;
        public DateTime? CreateDate { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyUser { get; set; }
    }
}
