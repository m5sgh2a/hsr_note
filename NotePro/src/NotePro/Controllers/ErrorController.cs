using Microsoft.AspNetCore.Mvc;

namespace NotePro.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult ErrorBadRequest()
        {
            return View("Error");
        }
    }
}