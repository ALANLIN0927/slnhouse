using System;
using System.Collections.Generic;

namespace prjhouse.Models
{
    public partial class Business
    {
        public int Fid { get; set; }
        public string? Businessmembername { get; set; }
        public string? Businessmemberphone { get; set; }
        public string? Businessmemberemail { get; set; }
        public string Type { get; set; } = null!;
        public decimal? Businessmoney { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
    }
}
