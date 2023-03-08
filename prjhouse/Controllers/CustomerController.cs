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

        public IActionResult create()
        {
            
            return View();

        }
        [HttpPost]
        public IActionResult create(ProductViewModel vm, IFormFile photo)
        {
            if (vm == null)
            {
                return View();
            }
            else
            {
                if (photo != null)
                {
                    string fileName = Guid.NewGuid().ToString() + ".jpg";
                    string filePath = Path.Combine(_environment.WebRootPath, "images", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                    vm.Housephoto = fileName;
                }

                //Product house = new Product();
                //house = vm.product;
              
                _house.Products.Add(vm.product);
                _house.SaveChanges();
                return Redirect("productlist");
            }


        }









        public IActionResult Index()
        {
            return View();
        }
    }
}
