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
        public IActionResult Login()
        {
            var userid = HttpContext.Session.GetString("userid");
            return View((object)userid);
        }
        //메인 
        public IActionResult Index()
        {
            return View();
        }
        //회원가입
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Login")]
        public IActionResult IsUser()
        {
           string userid = Request.Form["userid"];
           string userpw = Request.Form["userpassword"];
            var user = _context.Users.FirstOrDefault(u => u.Userid.Equals(userid.Trim()) && u.Pw.Equals(userpw));
            if (user != null)
            {
                //로그인 성공 시 , 세션에 정보 저장 (굳이 해야할까? 모르겠다)
                HttpContext.Session.SetInt32("userName", (int)user.Id); //사용자 uid(고유번호)
                HttpContext.Session.SetInt32("userId", user.Ban);  // 사용자 벤 여부 
                HttpContext.Session.SetInt32("userAdmin", user.Admin); // 관리자 여부
                //벤 유저 확인
                if (user.Ban == 1)
                {
                    HttpContext.Session.Remove("userid");
                }
                return View("Index",user);  
            }
            return View("IsUser");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userid"); 
            return View("Logout");
        }





    }

}

