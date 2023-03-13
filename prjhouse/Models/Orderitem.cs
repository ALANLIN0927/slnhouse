using System;
using System.Collections.Generic;

namespace prjhouse.Models
{
    public partial class Orderitem
    {
        public int Fid { get; set; }
        public int? OrdFid { get; set; }
        public int? ProductFid { get; set; }
        public int? Qty { get; set; }
        public decimal? ProductPrice { get; set; }
        public int? BussinessId { get; set; }
        public int? CustomerId { get; set; }
        public Product product { get; set; }
        public decimal? 小計 { get { return this.Qty * this.ProductPrice; } }

    }
}
