using Microsoft.AspNetCore.Mvc;

namespace dogcat.Models.ViewModels
{
    public class MapRequest : Controller
    {
        public string Address { get; set; } //사용자가 입력한  주소 
        public IActionResult Index()
        {
            return View();
        }
    }
}
