using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace prjhouse.Models
{
    public partial class Product
    {
        public int Fid { get; set; }
        [DisplayName("房子名稱")]
        public string? HouseName { get; set; }
        [DisplayName("地區")]
        public string? HouseAddressArea { get; set; }
        [DisplayName("城市")]
        public string? HouseAddressCity { get; set; }
        [DisplayName("價格")]
        public string? HousePrice { get; set; }
        [DisplayName("房子外觀")]
        public string? Housephoto { get; set; }
    }
}
