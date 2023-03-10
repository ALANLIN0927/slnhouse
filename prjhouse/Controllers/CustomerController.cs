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

        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult login()
        {   
            return View();
        }
        [HttpPost]
        public IActionResult login(customerloginViewModel loginmember)
        {
            if (loginmember.txtAccount == null || loginmember.txtPassword == null)
            {
                return View();
            }
            NormalMember member = _house.NormalMembers.FirstOrDefault(c => c.Phone == loginmember.txtAccount && c.Password == loginmember.txtPassword);
                if(member == null)
                 {
                return View();
                }
            else
            {
                string json = System.Text.Json.JsonSerializer.Serialize(member);
                HttpContext.Session.SetString(CDictionary.SK_LOGIN_USER, json);
                return Redirect("/Home/Index");
            }

        }


        public ActionResult loginmailverify(customerloginViewModel vm)                                                              //登入顯示會員狀態
        {
            NormalMember x = _house.NormalMembers.FirstOrDefault(c => c.Phone.Equals(vm.txtAccount) && c.Password.Equals(vm.txtPassword));
            if (x != null)
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




    }
}
