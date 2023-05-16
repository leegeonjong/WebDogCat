using Microsoft.AspNetCore.Mvc;

namespace dogcat.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
