using Microsoft.AspNetCore.Mvc;
using NotePro.Data;
using NotePro.Models;

namespace NotePro.Controllers
{
    public class RegisterController : Controller
    {
        private readonly AppDbContext context;

        public RegisterController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Register";

            return View();
        }

        [HttpPost]
        public ActionResult SubmitRegister(Register register)
        {
            if (ModelState.IsValid)
            {
                context.Add(register);
                context.SaveChanges();

                return RedirectToAction("Login", "Login");
            }

            return RedirectToAction("ErrorBadRequest", "Error");
        }
    }
}