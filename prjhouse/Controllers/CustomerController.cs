using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjhouse.Models;

namespace prjhouse.Controllers
{



    
    public class CustomerController : Controller
    {
        private readonly HouseContext _house;


        public CustomerController(HouseContext house)
        {
            _house = house;
        }




        public IActionResult productlist()
        {
            
            IEnumerable<Product> products = _house.Products;

            var productslist = from c in products
                               select c;

               return View(productslist);
        }

        public IActionResult create()
        {
            
            return View();

        }
        [HttpPost]
        public IActionResult create(Product product)
        {
            
            

        }









        public IActionResult Index()
        {
            return View();
        }
    }
}
