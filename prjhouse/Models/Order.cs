using System;
using System.Collections.Generic;

namespace prjhouse.Models
{
    public partial class Order
    {
        public int Fid { get; set; }
        public string? Orderaddress { get; set; }
        public string? Orderprice { get; set; }
        public string? Orderitemname { get; set; }
        public DateTime? Orderdate { get; set; }
        public int Memberid { get; set; }
        public int Itemid { get; set; }
    }
}
