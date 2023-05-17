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
            var userid = Request.Form["userid"];
            var userpw = Request.Form["userpassword"];
            return View();
        }

    }
}
