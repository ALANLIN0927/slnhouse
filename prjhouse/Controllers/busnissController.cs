using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using prjhouse.Models;
using prjhouse.ViewModels;
using System.Text.RegularExpressions;

namespace prjhouse.Controllers
{
    public class busnissController : Controller
    {
        private  IWebHostEnvironment _environment;
        private readonly HouseContext _house;

        public busnissController(IWebHostEnvironment environment, HouseContext house)
        {
            _environment = environment;
            _house = house;
        }

        public IActionResult Index()
        {
            return View();
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
            if (vm.HouseName == null || vm.HouseAddressArea == null || vm.HouseAddressCity == null || vm.HousePrice == null)
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

                Product house = new Product();
                house = vm.product;
                _house.Products.Add(vm.product);
                _house.SaveChanges();
                return RedirectToAction("productlist");
            }


        }
        public IActionResult checkformdata(Product vm)  //api
        {
            if (vm.HouseName == null)
            {
                return Json("房屋名稱不能空白");
            }
            if (vm.HouseAddressArea == null)
            {
                return Json("地區名稱不能空白");
            }
            if (vm.HouseAddressCity == null)
            {
                return Json("地區城市不能空白");
            }
            if (vm.HousePrice == null)
            {
                return Json("房屋售價不能空白");
            }

            return Json("已新增房屋");
        }

        public IActionResult edit(int? id)
        {
            if (id != null)
            {
                Product house = _house.Products.FirstOrDefault(c => c.Fid == id);
                if(house != null)
                {
                    return View(house);
                }

                return Redirect("productlist");
            }


            return Redirect("productlist");
        }

        [HttpPost]
        public IActionResult edit(Product h,ProductViewModel vm)
        {
            if (h.HouseName == null || h.HouseAddressCity==null||h.HouseAddressArea==null||h.HousePrice==null) 
            {
                return View(h);
            }
            Product house = _house.Products.FirstOrDefault(c=>c.Fid == h.Fid);
            if(house != null)
            {
                if (vm.photo != null)
                {
                    string fileName = Guid.NewGuid().ToString() + ".jpg";
                    string filePath = Path.Combine(_environment.WebRootPath, "images", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                       vm.photo.CopyTo(fileStream);
                    }
                    house.Housephoto = fileName;
                }
                else
                {
                    house.Housephoto = house.Housephoto;
                }
                house.HouseName = h.HouseName;
                house.HouseAddressCity = h.HouseAddressCity;
                house.HouseAddressArea = h.HouseAddressArea;
                house.HousePrice = h.HousePrice;
                _house.SaveChanges();
                return RedirectToAction("productlist");
            }
            return RedirectToAction("productlist");

        }
        
            public IActionResult Editverify(Product vm)
            {
            if (vm.HouseName == null)
            {
                return Json("房屋名稱不能空白");
            };
            if (vm.HouseAddressCity == null)
            {
                return Json("城市不能空白");
            }
            if (vm.HouseAddressArea == null)
            {
                return Json("區域不能空白");
            }
            if (vm.HousePrice == null)
            {
                return Json("價錢不能空白");
            }
            else
            {
                return Json("修改完成");
            };
           



        }

        public IActionResult delete(int? id)
        {
            if (id != null)
            {
                Product product= _house.Products.FirstOrDefault(c=>c.Fid==id);
                if(product!=null)
                {
                 _house.Products.Remove(product);
                    _house.SaveChanges();
                   
                }
                return RedirectToAction("productlist");
            }
            return RedirectToAction("productlist");
        }




    }

}
