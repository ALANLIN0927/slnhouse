using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using prjhouse.Models;
using prjhouse.ViewModels;
using System.Text.Json;


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

        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult login()
        {   
            return View();
        }
        [HttpPost]
        public IActionResult login(customerloginViewModel loginmember)                //登入 判斷公司或員工導向layout
        {
            string json = "";
            if (loginmember.txtAccount == null || loginmember.txtPassword == null)
            {
                return View();
            }
            NormalMember member = _house.NormalMembers.FirstOrDefault(c => c.Phone == loginmember.txtAccount && c.Password == loginmember.txtPassword);
            Business business = _house.Businesses.FirstOrDefault(b => b.Businessmemberphone == loginmember.txtAccount && b.Password==loginmember.txtPassword);
            if (member == null && business==null)
                {
                return View();
                }
            else
            {
                if(business!=null)
                {
                     json = System.Text.Json.JsonSerializer.Serialize(business);
                    HttpContext.Session.SetString(CDictionary.SK_LOGIN_USER, json);
                    return RedirectToAction("productlist", "busniss");
                }
                
                json = System.Text.Json.JsonSerializer.Serialize(member);
                HttpContext.Session.SetString(CDictionary.SK_LOGIN_USER, json);
                return RedirectToAction("productlist", "Customer");
            }







        }


        public ActionResult loginmailverify(customerloginViewModel vm)                //登入顯示會員狀態
        {   

            Business business=_house.Businesses.FirstOrDefault(b=>b.Businessmemberphone.Equals(vm.txtAccount)&&b.Password==vm.txtPassword);
            NormalMember x = _house.NormalMembers.FirstOrDefault(c => c.Phone.Equals(vm.txtAccount) && c.Password.Equals(vm.txtPassword));
            if (x != null || business!=null)
            {            
                        return Json("");                  
                }

            return Json("帳號或密碼有錯");
        }
        public IActionResult loginout()                                          //登出
        {

            HttpContext.Session.Remove(CDictionary.SK_LOGIN_USER);
            return Redirect("/Home/Index");
        }

        public IActionResult productlist(CKeywordViewModel vm)              //產品一覽
        {
            IEnumerable<Product> productlist = null;
            if (vm.txtKeyword == null)
            {
                productlist = from c in _house.Products select c;
            }
            else
            {
                string keyword = vm.txtKeyword;
                productlist = _house.Products.Where(c => c.HouseName.Contains(keyword)).ToList();
            }
            return View(productlist);         

        }
        public IActionResult productlistAPI(CKeywordViewModel vm)          //產品一覽供Index
        {
            IEnumerable<Product> productlist = null;
            if (vm.txtKeyword == null)
            {
                productlist = from c in _house.Products select c;
            }
            else
            {
                string keyword = vm.txtKeyword;
                productlist = _house.Products.Where(c => c.HouseName.Contains(keyword)).ToList();
            }
            return Json(productlist);

        }




        public IActionResult addtocarapi()                       //登入api
        {

            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGIN_USER))
            {
          
                return Json("可以加入購物車");
            }
            return Json("請先登入會員");


        }

        public IActionResult addshopcar(int? id)              //加入購物車
        {
            if (!(HttpContext.Session.Keys.Contains(CDictionary.SK_LOGIN_USER)))
            {
                return RedirectToAction("productlist");
            }

            Product house= _house.Products.FirstOrDefault(c=>c.Fid==id);
            if (house==null)
            {
                return RedirectToAction("productlist");
            }
            List<Product> cart = null;
            string json = "";
            if(HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS_LIST)) 
            {
                json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST);
                cart = JsonSerializer.Deserialize<List<Product>>(json);          
            }
            else
            {
                cart = new List<Product>();
            }
             cart.Add(house);
             json=JsonSerializer.Serialize(cart);
             HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST,json);
           return RedirectToAction("productlist");
        }
        public IActionResult cartview()                                          //購物車一覽
        {

            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGIN_USER)){

                List<Product> cart = null;
                string json = "";
                if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS_LIST))
                {
                    json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST);
                    cart = JsonSerializer.Deserialize<List<Product>>(json);
                    return View(cart);
                }
                else
                {
                    return RedirectToAction("productlist");
                }
            }

            return RedirectToAction("productlist");
        }
        //public IActionResult deletecaritem(int id)
        //{
        //    Product cartitem=_house.NormalMembers






        //}

    }
}
