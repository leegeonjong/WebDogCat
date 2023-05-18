using dogcat.Models.Domain;
using dogcat.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dogcat.Controllers
{
    public class MyPageController : Controller
    {
        private readonly IUserRepositories _userRepositories;

        public MyPageController(IUserRepositories userRepositories)
        {
            _userRepositories = userRepositories;
        }
        [HttpGet]
        public async Task<IActionResult> home(long id)
        {
            var user = await _userRepositories.GetUserAsync(id);
            if (user == null) return null;
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> Page(long id)
        {
            var user = await _userRepositories.GetUserAsync(id);
            if (user == null) return null;
            return View(user);
        }
        [HttpPost]
        [ActionName("Page")]
        public async Task<IActionResult> Delete(long id)
        {
            await _userRepositories.DeleteAsync(id);
            return RedirectToAction("Page", new {id = 2});  // 완료하면 메인페이지로 넘어가야한다. 지긍은 임시
        }
        [HttpGet]
        public async Task<IActionResult> Detail(long id)
        {
            var user = await _userRepositories.GetUserAsync(id);
            return View(user);
        }
        [HttpPost]
        [ActionName("Detail")]
        public async Task<IActionResult> Detail(User user)
        {
            var update = await _userRepositories.UpdateAsync(user);
            return RedirectToAction("page", new {id = user.Id });
        }
    }
}
