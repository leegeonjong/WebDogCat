using Microsoft.AspNetCore.Mvc;

namespace dogcat.Controllers
{
    public class UserController : Controller
    {
        //로그인 
        public IActionResult Login()
        {
            var userid = HttpContext.Session.GetString("userid");
            return View((object)userid);
        }

        //로그인 확인
        [HttpPost]
        [ActionName("Login")]
        public IActionResult IsUser()
        {
            var userid = Request.Form["userid"]; //form에서 id입력값
            var userpw = Request.Form["userpassword"]; // form에서 비번 입력값
            return View();
        }

    }
}
