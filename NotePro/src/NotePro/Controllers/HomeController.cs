using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotePro.Models;

namespace NotePro.Controllers
{
    public class HomeController : Controller
    {
        private NoteProContext context;

        public HomeController(NoteProContext context)
        {
            this.context = context;
        }

        public async Task<string> Baum()
        {
            var author = await context.Authors
                .Include(x => x.Notes)
                .ToArrayAsync();

            var response = author.Select(x => new
            {
                firstName = x.FirstName,
                lastName = x.LastName,
                notes = x.Notes.Select(u => u.Title).FirstOrDefault()
            });

            string name = string.Empty;

            foreach (var element in response)
            {
                name += String.Format("First name: {0} \nLast name: {1} \nNotes: {2}\n", element.firstName, element.lastName, element.notes);
            }

            return name;
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

        public IActionResult Error()
        {
            return View();
        }
    }
}
