using Microsoft.AspNetCore.Mvc;
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
            
           
            return View();
        }







        public IActionResult Index()
        {
            return View();
        }
    }
}
