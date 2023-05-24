using dogcat.Data;
using dogcat.Models;
using dogcat.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.IO;

namespace dogcat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DogcatDbContext _dogcatDbContext;

        public HomeController(ILogger<HomeController> logger, DogcatDbContext dogcatDbContext)
        {
            _logger = logger;
            _dogcatDbContext = dogcatDbContext;
        }

        public async Task<IActionResult> Index()
        {
            //var writes = await _dogcatDbContext.Writes.Where(x => x.User.Admin == 1).OrderByDescending(x => x.Time).ToListAsync();
            var list = await _dogcatDbContext.Writes
                        .Include(x => x.User)
                        .ToListAsync();
            foreach(var i in list)
            {
                i.RequestPath = $"/appfiles/{i.Image}";
            }
            return View(list);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}