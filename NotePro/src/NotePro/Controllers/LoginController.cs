using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NotePro.Data;
using NotePro.Models;
using System.Security.Claims;

namespace NotePro.Controllers
{
    public class LoginController : Controller
    {
        private NoteProContext context;

        public LoginController(NoteProContext context)
        {
            this.context = context;
        }

        public IActionResult Login()
        {
            ViewData["Title"] = "Login";

            return View();
        }

        public IActionResult ForgotPassword()
        {
            return RedirectToAction("Index", "");
        }

        [HttpPost]
        public IActionResult SubmitLogin(AuthorLogin author)
        {
            if (ModelState.IsValid)
            {
                context.Add(author);
                context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("ErrorBadRequest", "Error");
        }
    }
}