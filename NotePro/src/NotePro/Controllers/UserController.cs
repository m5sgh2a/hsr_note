using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NotePro.Data;
using NotePro.Models;

namespace NotePro.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext context;

        public UserController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Register()
        {
            ViewData["Title"] = "Register";

            return View();
        }

        [HttpPost]
        public IActionResult SubmitRegister(Register model)
        {
            if (ModelState.IsValid)
            {
                context.Add(model);
                context.SaveChanges();

                return RedirectToAction("Login", "User");
            }

            return RedirectToAction("ErrorBadRequest", "Error");
        }

        public IActionResult Login()
        {
            ViewData["Title"] = "Login";

            return View();
        }

        [HttpPost]
        public IActionResult SubmitLogin(Login model)
        {
            if (ModelState.IsValid)
            {
                var loginExist = context.Login
                    .Where(x => String.Compare(x.Email, model.Email, true) == 0
                        && x.Password == model.Password).Count();

                if (loginExist > 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }

            return RedirectToAction("ErrorBadRequest", "Error");
        }
    }
}
