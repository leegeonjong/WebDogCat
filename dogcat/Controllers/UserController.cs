using dogcat.Data;
using dogcat.Models.Domain;
using dogcat.Models.ViewModels;
using dogcat.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace dogcat.Controllers
{
    public class UserController : Controller
    {

        //user repository
        private readonly IUserRepositories _userRepository;
        //controller
        public UserController(IUserRepositories userRepository)
        {
            _userRepository = userRepository;
        }


        //메인 View
        public IActionResult Index()
        {
            return View();
        }
        //회원가입 View
        public IActionResult Register()
        {
            return View();
        }





        //회원가입 
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

