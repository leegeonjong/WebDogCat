using dogcat.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dogcat.Controllers
{
    public class MyPetController : Controller
    {
        private readonly IPetRepositories _petRepositories;

        public MyPetController(IPetRepositories petRepositories)
        {
            _petRepositories = petRepositories;
        }
        [HttpGet]
        public async Task<IActionResult> MypetPage()
        {
            var list = await _petRepositories.GetAllAsync(8);
            return View(list);
        }
    }
}
