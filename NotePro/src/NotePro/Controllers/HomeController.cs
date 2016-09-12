﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotePro.Models;
using NotePro.Data;

namespace NotePro.Controllers
{
    public class HomeController : Controller
    {
        private NoteProContext context;

        public HomeController(NoteProContext context)
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
