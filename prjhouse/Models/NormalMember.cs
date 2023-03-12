using System;
using System.Collections.Generic;

namespace prjhouse.Models
{
    public partial class NormalMember
    {
        public int FId { get; set; }
        public string? MemberName { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? Gender { get; set; }
        public string? AddressCity { get; set; }
        public string? AddressArea { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Email { get; set; }
        public string? MemberPhotoFile { get; set; }
        public string? Emailtified { get; set; }
        public string? GoogleEmail { get; set; }
        public string? LineUserid { get; set; }
        public string Type { get; set; } = null!;
        public decimal? Membermoney { get; set; }
    }
}
