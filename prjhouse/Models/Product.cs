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
        public string? HousePrice { get; set; }
    }
}
