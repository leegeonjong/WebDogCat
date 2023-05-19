using dogcat.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace dogcat.Controllers
{
    public class LoginController : Controller
    {
        //DbContext
        private readonly DogcatDbContext _context;
        //controller 생성자
        public LoginController(DogcatDbContext context)
        {
            _context = context;
        }
        //로그인 View
        public IActionResult Login()
        {
            var userid = HttpContext.Session.GetString("userid");
            return View((object)userid);
        }
        //메인 View
        public IActionResult Index()
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
                HttpContext.Session.SetInt32("userId", (int)user.Id); //사용자 uid(고유번호)
                HttpContext.Session.SetInt32("userBan", user.Ban);  // 사용자 벤 여부 
                HttpContext.Session.SetInt32("userAdmin", user.Admin); // 관리자 여부
                //벤 유저 확인
                if (user.Ban == 1) //벤
                {
                    HttpContext.Session.Remove("userId"); //세션 정보 삭제
                    return View("Index", user);
                }
                else // 벤 x
                {
                    return View("Index", user);
                }
            }
            return View("IsUser");

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userid");
            return View("Logout");
        }
        //ID찾기
        public IActionResult FindId() //View
        {
            return View();
        }

        [HttpPost]
        [ActionName("FindId")]
        public IActionResult Find_Id()
        {
            string name = Request.Form["inputname"];
            string mail = Request.Form["inputmail"];
            var user = _context.Users.FirstOrDefault(x => x.Name.Equals(name.Trim()) && x.Mail.Equals(mail.Trim()));
                
            return View("ResultId", user);
        }
        
        public IActionResult ResultId()
        {
            return View();
        }

        public IActionResult Findpw()
        {
            return View();
        }

        [HttpPost]
        [ActionName("FindPw")]
        public IActionResult Find_Pw()
        {
            string userid = Request.Form["inputid"];
            string mail = Request.Form["inputmail"];
            var user = _context.Users.FirstOrDefault(x => x.Userid.Equals(userid.Trim()) && x.Mail.Equals(mail.Trim()));
            return View("ResultPw", user);
        }
        public IActionResult ResultPw()
        {
            return View();
        }

    } // end controller
}
