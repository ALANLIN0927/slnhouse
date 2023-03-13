using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using prjhouse.Models;
using prjhouse.ViewModels;
using System.Text.Json;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        public IActionResult addshopcar(int id)
        {
            ViewBag.fId = id;
            return View();
        }


        [HttpPost]
        public IActionResult addshopcar(CaddtocartViewModel p)              //加入購物車
        {
            if (!(HttpContext.Session.Keys.Contains(CDictionary.SK_LOGIN_USER)))
            {
                return RedirectToAction("productlist");
            }

            Product house = _house.Products.FirstOrDefault(c => c.Fid == p.txtFid);
            if (house == null)
            {
                return RedirectToAction("productlist");
            }
            List<Orderitem> cart = null;
            string json = "";
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS_LIST))
            {
                json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST);
                cart = JsonSerializer.Deserialize<List<Orderitem>>(json);
            }
            else
            {
                cart = new List<Orderitem>();
            }
            Orderitem x = new Orderitem();
            x.Fid = house.Fid;
            x.BussinessId = house.Housebusnissfid;
            x.Qty = p.txtCount;
            x.ProductPrice = house.HousePrice;
            x.ProductFid= p.txtFid;
            x.product = house;
            
            cart.Add(x);
            json = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST, json);
            return RedirectToAction("productlist");
        }
        public IActionResult cartview()                                          //購物車一覽
        {
            
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_LOGIN_USER))
            {

                List<Orderitem> cart = null;
                string json = "";
                if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS_LIST))
                {
                    json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST);
                    cart = JsonSerializer.Deserialize<List<Orderitem>>(json);
                    return View(cart);
                }
                else
                {
                    return RedirectToAction("productlist");
                }
            }

            return RedirectToAction("productlist");
        }

        public IActionResult checkout()
        {
            string jsonmember = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            NormalMember shopmember = JsonSerializer.Deserialize<NormalMember>(jsonmember);

            List<Orderitem> cart = null;
            string json = "";
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS_LIST))
            {
                json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST);
                cart = JsonSerializer.Deserialize<List<Orderitem>>(json);


                foreach (var item in cart)
                {
                    Orderitem x = new Orderitem();
                    x.Qty = item.Qty;
                    x.ProductPrice = item.ProductPrice;
                    x.OrdFid = item.OrdFid;
                    x.ProductFid = item.Fid;
                    x.ProductFid = item.ProductFid;
                    x.CustomerId = shopmember.FId;
                    x.BussinessId = item.BussinessId;
                    _house.Add(x);
                    _house.SaveChanges();
                }
                return RedirectToAction("productlist");
            }

            return RedirectToAction("productlist");

        }

            public IActionResult looklist()               //訂單一覽
           {
               

            string jsonmember = HttpContext.Session.GetString(CDictionary.SK_LOGIN_USER);
            NormalMember shopmember = JsonSerializer.Deserialize<NormalMember>(jsonmember);

            var list = from b in _house.Businesses
                       join o in _house.Orderitems on b.Fid equals o.BussinessId                     
                       from n in _house.NormalMembers
                       join o2 in _house.Orderitems on n.FId equals o2.CustomerId       
                       from p in _house.Products
                       join o3 in _house.Orderitems on p.Fid equals o3.ProductFid                  
                       where n.FId == shopmember.FId && n.FId==o2.CustomerId && p.Fid== o3.ProductFid &&b.Fid==o.BussinessId
                       select new
                       {
                          
                          b.Fid,                        
                          p.HousePrice
                       };

                        List<Order> listorderview = new List<Order>();
                        if (list == null)
                            {
                            return RedirectToAction("productlist");
                            }
                    foreach(var item in list.Distinct())
                    {
                        Order single = new Order();
                        single.Ordertotalprice = item.HousePrice.ToString();                      
                        single.Bussnisid = item.Fid;
                        
                        
                        listorderview.Add(single);
                        _house.Orders.Add(single);
                        
                    }
                    _house.SaveChanges();
                    return View(listorderview);




        }




    }
}
