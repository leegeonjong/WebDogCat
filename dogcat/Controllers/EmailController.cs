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
                Email = (string)TempData["Email"],
                InputPassword = (string)TempData["InputPassword"],

            };
            ViewData["page"] = HttpContext.Session.GetInt32("page") ?? 1;
            return View(emailRequest);
        }
        // POST: /Board/Email
        [HttpPost]
        [ActionName("Email")]
        public async Task<IActionResult> Send(EmailRequest emailRequest)
        {
            //메일 보내기 
            emailRequest.SendEmail();
            //TempData에 작성자가 작성한 email 과 RealPassword 저장하기
            TempData["Email"] = emailRequest.Email;
            TempData["RealPassword"] = emailRequest.RealPassword;

            return RedirectToAction("Email");
        }

        [HttpPost]
        [ActionName("EmailCheck")]
        public async Task<IActionResult> EmailCheck(EmailRequest emailRequest)
        {
            // TempData 에 담아둔 RealPassword 꺼내가기
            emailRequest.RealPassword = (string)TempData["RealPassword"];

            //검증
            if (emailRequest.CheckEmail())
            {
                await HttpContext.Response.WriteAsync("<script>alert('OK!');</script>");
                return RedirectToAction("Login");
            }
            else
            {
                await HttpContext.Response.WriteAsync("<script>alert('fail!');</script>");
                return RedirectToAction("Email");
            }
        }

    }//end
}
