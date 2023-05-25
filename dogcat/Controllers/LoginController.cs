using Azure.Core;
using dogcat.Data;
using dogcat.Models.Domain;
using dogcat.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;

namespace dogcat.Controllers
{
    public class LoginController : Controller
    {
        //이메일 인증 
        public string RealPassword { get; set; }
        public string InputCode { get; set; }
        private readonly DogcatDbContext _context;
        private readonly ILogger<HomeController> _logger;
        //controller 생성자
        public LoginController(DogcatDbContext context, ILogger<HomeController> logger) //사용
        {
            _context = context;
            _logger = logger;
        }
        //로그인 View
        public IActionResult Login() //사용
        {
            return View();
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
            List<dogcat.Models.Domain.Write> writes = _context.Writes.ToList();
            var user = _context.Users.FirstOrDefault(u => u.Userid.Equals(userid.Trim()) && u.Pw.Equals(userpw));
            if (user != null)
            {
                //로그인 성공 시, 세션에 정보 저장
                HttpContext.Session.SetInt32("userId", (int)user.Id); //사용자 uid(고유번호)
                HttpContext.Session.SetString("userNickName", user.NickName); //사용자 닉네임
                HttpContext.Session.SetInt32("userBan", user.Ban); // 사용자 벤 여부
                HttpContext.Session.SetInt32("userAdmin", user.Admin); // 관리자 여부

                //벤 유저 확인
                if (user.Ban == 1) //벤
                {
                    HttpContext.Session.Remove("userId"); //세션 정보 삭제
                    return View("Index", user);
                }
                else // 벤 x
                {
                    ViewData["user"] = user;
                    return View("IsUser", user);
                }
            }
            return View("IsUser");
        }


        //아이디 중복 검사
        //[HttpPost]
        public IActionResult Idcheck(string Userid)
        {
            string input_id = Userid;
            var user = _context.Users.FirstOrDefault(x => x.Userid.Trim().Equals(input_id)); //비교 (중복검사)
            if (user != null) return Json("unable"); // 중복이면 JSON에 unable 넘겨주기
            else
            {
            return Json("able"); // 중복 아니면 JSON에 able 넘겨주기
            }

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userId");
            return View("Logout");
        }

        //ID찾기
        public IActionResult FindId() //View 사용
        {
            return View();
        }


        public IActionResult Findpw() //사용
        {
            return View();
        }

        [HttpPost]
        [ActionName("ResultPw")]
        public IActionResult ResultPw2(string pw)//form 에서 pw 값 가져오기 사용
        {
            // 세션에서 ID와 이메일 값 가져오기
            string userId = HttpContext.Session.GetString("Id");
            string mail = HttpContext.Session.GetString("Email");

            // 데이터베이스에서 사용자 찾기
            var user = _context.Users.FirstOrDefault(x => x.Userid == userId.Trim() && x.Mail == mail.Trim());
            // 사용자를 찾았다면 
            if (user != null)
            {
                string new_pw = user.Pw;

                // 비밀번호 업데이트
                user.Pw = pw;
                _context.SaveChanges();
            }

            return RedirectToAction("Login", "User");
        }


    } // end controller
}
