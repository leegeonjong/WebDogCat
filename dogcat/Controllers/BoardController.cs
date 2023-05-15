using Microsoft.AspNetCore.Mvc;

namespace dogcat.Controllers
{
    public class BoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
