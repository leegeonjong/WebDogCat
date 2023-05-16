using Microsoft.AspNetCore.Mvc;

namespace dogcat.Models.ViewModels
{
    public class MapRequest : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
