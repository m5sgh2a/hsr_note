using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NotePro.Data;
using NotePro.Models;
using System.Security.Claims;
using System.Linq;

namespace NotePro.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext context;

        public LoginController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Login()
        {
            ViewData["Title"] = "Login";

            return View();
        }

        [HttpPost]
        public IActionResult SubmitLogin(Login login)
        {
            if (ModelState.IsValid)
            {
                var loginExist = context.Login
                    .Where(x => String.Compare(x.Email, login.Email, true) == 0
                        && x.Password == login.Password).Count();

                if (loginExist > 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }

            return RedirectToAction("ErrorBadRequest", "Error");
        }

        [HttpPost]
        public IActionResult SubmitRegister(Register register)
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