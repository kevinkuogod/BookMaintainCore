using System;
using System.Collections.Generic;

namespace bookMaintain.Model.Models
{
    public partial class Customer
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int? Role { get; set; }
        public string Email { get; set; } = null!;
        public DateTime? EmailVerifiedAt { get; set; }
        public string? Telephone { get; set; }
        public string? Fax { get; set; }
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public string RememberToken { get; set; } = null!;
        public DateTime? CreateDate { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyUser { get; set; }
    }
}
