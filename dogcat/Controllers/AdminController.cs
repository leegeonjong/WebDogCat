using dogcat.Models.Domain;
using dogcat.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dogcat.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepositories _adminRepositories;

        public AdminController(IAdminRepositories adminRepositories)
        {
            _adminRepositories = adminRepositories;
        }
        [HttpGet]
        public async Task<IActionResult> AdminAlluser(long id)
        
        {
            var user = await _adminRepositories.AlluserAsync(id);
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(long id)
        {
            var user = await _adminRepositories.GetuserAsync(id);
            return View(user);
        }
        [HttpPost]
        [ActionName("Detail")]
        public async Task<IActionResult> Ban(long id)
        {
            var user = await _adminRepositories.UserBanAsync(id);
            
            return RedirectToAction("Detail", new { id = user.Id });
        }
        
    }
}
