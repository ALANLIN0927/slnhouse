using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace prjhouse.Models
{
    public partial class Product
    {
        public int Fid { get; set; }
        [DisplayName("產品名")]
        public string? HouseName { get; set; }
        [DisplayName("地區")]
        public string? HouseAddressArea { get; set; }
        [DisplayName("城市")]
        public string? HouseAddressCity { get; set; }
        [DisplayName("價錢")]
        public decimal? HousePrice { get; set; }
        [DisplayName("照片")]
        public string? Housephoto { get; set; }
        public int? Housebusnissfid { get; set; }
    }
}
