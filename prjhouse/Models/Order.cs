using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace prjhouse.Models
{
    public partial class Order
    {
        public int Fid { get; set; }
        public string? Ordertotalprice { get; set; }
        public DateTime? Orderdate { get; set; }
        public int Memberid { get; set; }
        public int Bussnisid { get; set; }
        [DisplayName("公司名")]
        public string? BussnisName { get; set; }
        public int? Itemfid { get; set; }
        [DisplayName("產品名")]
        public string? ProductName { get; set; }
        [DisplayName("產品單價")]
        public decimal? Productprice { get; set; }
        [DisplayName("數量")]
        public int? Productcount { get; set; }
    }
}
