using dogcat.Data;
using dogcat.Models.Domain;
using dogcat.Models.ViewModels;
using dogcat.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Net.Mail;
using System.Net;

namespace dogcat.Controllers
{
    public class UserController : Controller
    {

        //user repository
        private readonly IUserRepositories _userRepository;
        //DbContext
        private readonly DogcatDbContext _context;
        //controller
        public UserController(IUserRepositories userRepository, DogcatDbContext dbContext)
        {
            _context = dbContext;
            _userRepository = userRepository;
        }
        //이메일 인증 
        public string RealPassword { get; set; }
        public string InputCode { get; set; }

        //로그인 
        public IActionResult Login()
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
                HttpContext.Session.SetString("userNickName", user.NickName); //사용자 닉네임
                HttpContext.Session.SetInt32("userBan", user.Ban);  // 사용자 벤 여부 
                HttpContext.Session.SetInt32("userAdmin", user.Admin); // 관리자 여부
                //벤 유저 확인
                if (user.Ban == 1) //벤
                {
                    HttpContext.Session.Remove("userId"); //세션 정보 삭제
                    return View();
                }
                else // 벤 x
                {
                    return View("Index", user);
                }
            }
            return View("IsUser");

        }
        //-------------------------------------------------------------------------------------------------
        //로그아웃
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userId");
            return View("Logout");
        }
        //-----------------------------------------------------------------------------------------------

        //메인 View
        public IActionResult Index()
        {
            return View();
        }
        //---------------------------------------------------------------------------------------------------

        //아이디 중복 검사
        
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
        //-------------------------------------------------------------------------------------------------------

        //이메일 발송
        [HttpPost]
        [ActionName("Send")]
        public void SendMail(string input_mail)
        {
            // 이메일 보내는 사람의 구글 이메일 주소
            string fromEmail = "lateaksoo@gmail.com";
            // 이메일 보내는 사람의 구글 앱 비밀번호 
            string fromPassword = "hikhvuxhscwwacew";
            // 이메일 받는 사람의 이메일 주소
            string toEmail = input_mail; //Request.Form["inputmail"];
            // 이메일 제목
            string subject = "이메일 인증번호 안내 입니다.";
            //인증번호 (랜덤숫자 6자리)
            RealPassword = ((int)Math.Floor(new Random().NextDouble() * 10000000)).ToString();
            // 이메일 내용
            string body = $"인증번호 안내 : {RealPassword}";
            // 이메일 메시지 객체 생성
            MailMessage message = new MailMessage();

            message.To.Add(toEmail);
            message.From = new MailAddress(fromEmail);
            message.Subject = subject;
            message.Body = body;
            // 이메일 메시지 보내기
            using (var client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.Credentials = new NetworkCredential(fromEmail, fromPassword);
                client.Send(message);
            }
        }
    //---------------------------------------------------------------------------------------------------------

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
            InputCode = Request.Form["inputverify"];
            var user = _context.Users.FirstOrDefault(x => x.Name.Equals(name.Trim()) && x.Mail.Equals(mail.Trim()));
            if (InputCode != RealPassword)
            {
                TempData["message"] = "인증코드 불일치!";
                return View();
            }
            else
            {
                return View("ResultId", user);
            }

        }
        //-------------------------------------------------------------------------------------------
        //id 찾기 결과
        public IActionResult ResultId()
        {
            return View();
        }
        //-----------------------------------------------------------------------------------------
        //비밀번호 찾기
        public IActionResult Findpw()
        {
            return View();
        }
        [HttpPost]
        [ActionName("FindPw")]
        public async Task<IActionResult> Find_Pw()
        {
            string userid = Request.Form["inputid"];
            string mail = Request.Form["inputmail"];
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Userid == userid.Trim() && x.Mail == (mail.Trim()));
            return RedirectToAction("ResultPw", user);

        }
        //---------------------------------------------------------------------------------------------
        //비밀번호 찾기 결과
        public IActionResult ResultPw(User user)
        {
            return View();
        }
        [HttpPost]
        [ActionName("ResultPw")]
        public IActionResult ResultPw2(User user)
        {
            string new_pw = Request.Form["pw"];
            var _user = _context.Users.FirstOrDefault(x => x.Id == user.Id);
            _user.Pw = new_pw;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        //----------------------------------------------------------------------------------------------
        //회원가입 
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Register")]
        public async Task<IActionResult> Register(AddUserRequest addWriteRequest)
        {
            var user = new User()
            {
                Userid = addWriteRequest.Userid,
                Pw = addWriteRequest.Pw,
                Name = addWriteRequest.Name,
                NickName = addWriteRequest.NickName,
                PhoneNum = addWriteRequest.PhoneNum,
                Mail = addWriteRequest.Mail,
            };

            user = await _userRepository.AddUserAsync(user);
            return RedirectToAction("Index");
        }
        //--------------------------------------------------------------------------------------------

        //비밀번호 찾기 (수정)
        [HttpPost]
        [ActionName("UpdatePw")]
        public async Task<IActionResult> UpdatePw(EditPwRequest request)
        {
            var user = new User()
            {
                Pw = request.NewPassword
            };
            
            return RedirectToAction("Index");


        }

    }

}








































