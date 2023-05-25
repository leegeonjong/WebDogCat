using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using dogcat.Models.ViewModels;

namespace dogcat.Controllers
{
    public class EmailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Email()
        {
            EmailRequest emailRequest = new()
            {
                // TempData 에 담아둔 내용 꺼내가기  (꺼내가면 자동 소멸됨)
                Id = (string)TempData["Id"] ?? null,
                Email = (string)TempData["Email"],
                InputPassword = (string)TempData["InputPassword"],

            };
            if (emailRequest.Id == null)
            {
                ViewData["page"] = HttpContext.Session.GetInt32("page") ?? 1;
                return View("~/Views/Login/FindId.cshtml", emailRequest);
            }
            else
            {
                TempData["Id"] = emailRequest.Id;
                TempData["Email"] = emailRequest.Email;
                return View("~/Views/Login/FindPw.cshtml", emailRequest);
            }
        }

        // POST: /Email/Email
        [HttpPost]
        [ActionName("Email")]
        public async Task<IActionResult> Send(EmailRequest emailRequest)
        {
            //메일 보내기 
            emailRequest.SendEmail();
            // Session에 작성자가 작성한 name, email, Id와 RealPassword 저장하기
            HttpContext.Session.SetString("Name", emailRequest.Name);
            HttpContext.Session.SetString("Email", emailRequest.Email);
            HttpContext.Session.SetString("RealPassword", emailRequest.RealPassword);

            NullCheck(emailRequest);

            if (emailRequest.Id == null)
            {
                return View("~/Views/Login/FindId.cshtml", emailRequest);
            }
            else
            {
                return View("~/Views/Login/FindPw.cshtml", emailRequest);
            }
        }

        [HttpPost]
        [ActionName("EmailCheck")]
        public async Task<IActionResult> EmailCheck(EmailRequest emailRequest)
        {
            // TempData 에 담아둔 RealPassword 꺼내가기
            emailRequest.RealPassword = HttpContext.Session.GetString("RealPassword");
            emailRequest.Id = HttpContext.Session.GetString("Id");
            emailRequest.Email = HttpContext.Session.GetString("Email");
            emailRequest.Name = HttpContext.Session.GetString("Name");

            //session 에 저장
            HttpContext.Session.SetString("Name", emailRequest.Name);
            HttpContext.Session.SetString("Email", emailRequest.Email);

            NullCheck(emailRequest);

            //검증
            if (emailRequest.CheckEmail())
            {
                //통과
                if (emailRequest.Id == null)
                {
                    //아이디찾기
                    return View("~/Views/Login/ResultId.cshtml");
                }
                else
                {
                    //비밀번호 찾기
                    return View("~/Views/Login/ResultPw.cshtml");
                }
            }
            else
            {
                //다시 찾기
                if (emailRequest.Id == null)
                {
                    return View("~/Views/Login/FindId.cshtml");
                }
                else
                {
                    return View("~/Views/Login/FindPw.cshtml");
                }
            }
        }

        //session null일때 처리함수
        private void NullCheck(EmailRequest emailRequest)
        {
            if (emailRequest.Id == null)
            {
                HttpContext.Session.Remove("Id");
            }
            else
            {
                HttpContext.Session.SetString("Id", emailRequest.Id);
            }
            if (emailRequest.Name == null)
            {
                HttpContext.Session.Remove("Name");
            }
            else
            {
                HttpContext.Session.SetString("Name", emailRequest.Name);
            }
        }

    }//end
}
