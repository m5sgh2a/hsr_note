﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotePro.Data;
using NotePro.Models;

namespace NotePro.Controllers
{
    [AllowAnonymous]
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
                var user = context.Login
                    .Where(x => String.Compare(x.Email, model.Email, true) == 0
                        && x.Password == model.Password).FirstOrDefault();

                if (user != null)
                {
                    List<Claim> userClaims = new List<Claim>()
                    {
                        new Claim("UserId", user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.Role, "user")
                    };

                    ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(userClaims, "local"));
                    HttpContext.Authentication.SignInAsync("CookieAuth", principal);

                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("ErrorBadRequest", "Error");
        }
    }
}
