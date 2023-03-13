using System;
using System.Collections.Generic;

namespace prjhouse.Models
{
    public partial class Product
    {
        public int Fid { get; set; }
        public string? HouseName { get; set; }
        public string? HouseAddressArea { get; set; }
        public string? HouseAddressCity { get; set; }
        public decimal? HousePrice { get; set; }
        public string? Housephoto { get; set; }
        public int? Housebusnissfid { get; set; }
    }
}
