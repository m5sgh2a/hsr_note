using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotePro.Data;

namespace NotePro.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private AppDbContext context;

        public HomeController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Home";

            return View();
        }

        public IActionResult About()
        {
            ViewData["Title"] = "Über";

            return View();
        }
    }
}
