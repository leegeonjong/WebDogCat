using dogcat.Data;
using dogcat.Models.Domain;
using dogcat.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace dogcat.Controllers
{
    public class UserController : Controller
    {

        private readonly DogcatDbContext _context;
        public UserController(DogcatDbContext context) 
        {
            _context = context;
        }

        //로그인 폼
        [HttpGet]
        public IActionResult Login()
        {
            var username = HttpContext.Session.GetString("userid");
            return View((object)username);
        }
        [HttpPost]
        [ActionName("Login")]
        public IActionResult LoginOk()
        {
           string username = Request.Form["userid"];
           string userpw = Request.Form["userpassword"];
            var user = _context.Users.FirstOrDefault(u => u.Userid == username.Trim() && u.Pw == userpw);
            if (user != null)
            {
                //로그인 성공 시 , 세션에 정보 저장
                HttpContext.Session.SetString("userid", username);
                return View("IsUser", true);
            }
            HttpContext.Session.Remove("userid");
            return View("IsUser");
        }




    }

}

