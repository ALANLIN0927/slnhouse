using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjhouse.Models;
using prjhouse.ViewModels;

namespace prjhouse.Controllers
{



    
    public class CustomerController : Controller
    {
        private readonly HouseContext _house;
        private IWebHostEnvironment _environment;

        public CustomerController(HouseContext house,IWebHostEnvironment environment)
        {
            _house = house;
            _environment = environment;
        }

         public IActionResult productlist()
        {
            
            IEnumerable<Product> products = _house.Products;

            var productslist = from c in products
                               select c;

               return View(productslist);
        }
       

        public IActionResult Index()
        {
            return View();
        }
    }
}
