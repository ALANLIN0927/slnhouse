using prjhouse.Models;

namespace prjhouse.ViewModels
{
    public class ProductViewModel
    {
        private  Product _product;

        public  ProductViewModel()
        {
            _product = new Product();

        }
        public Product product
        {
            get{return _product;}
            set{ _product = value; }


        }

         public int Fid
        {
            get { return _product.Fid; }
            set { _product.Fid = value; }
        }

        public string? HouseName 
        {   get { return _product.HouseName; }
            set {_product.HouseName=value; } 
        }
        
        public string? HouseAddressArea
        {   get { return _product.HouseAddressArea; }
            set { _product.HouseAddressArea = value; }
        }
       
        public string? HouseAddressCity 
        {   
            get {return _product.HouseAddressCity; } 
            set { _product.HouseAddressCity = value; } 
        }

        public string? HousePrice 
        {  
            get {return _product.HousePrice; }
            set {_product.HousePrice=value; } 
        }













    }
}
