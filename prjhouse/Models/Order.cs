using System;
using System.Collections.Generic;

namespace prjhouse.Models
{
    public partial class Order
    {
        public int Fid { get; set; }
        public string? Ordertotalprice { get; set; }
        public DateTime? Orderdate { get; set; }
        public int Memberid { get; set; }
        public int Bussnisid { get; set; }
    }
}
