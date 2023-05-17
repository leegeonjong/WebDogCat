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
                HttpContext.Session.SetInt32("userName", (int)user.Id); //사용자 uid(고유번호)
                HttpContext.Session.SetInt32("userId", user.Ban);  // 사용자 벤 여부 
                HttpContext.Session.SetInt32("userAdmin", user.Admin); // 관리자 여부

                return View("Index",user);
            }
            HttpContext.Session.Remove("userid");
            return View("IsUser");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userid");
            return View("Index");
        }




    }

}

