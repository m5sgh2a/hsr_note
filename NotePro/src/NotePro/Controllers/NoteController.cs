using Microsoft.AspNetCore.Mvc;
using NotePro.Models;
using NotePro.Data;

namespace NotePro.Controllers
{
    public class NoteController : Controller
    {
        private NoteProContext context;

        public NoteController(NoteProContext context)
        {
            this.context = context;
        }

        public IActionResult NewNote()
        {
            ViewData["Title"] = "Neue Notiz";

            return View();
        }

        public IActionResult ManageNotes()
        {
            ViewData["Title"] = "Notizen Verwalten";

            return View();
        }

        public IActionResult Cancel()
        {
            return RedirectToAction("Index",""); //TODO: go back where you come from
        }

        [HttpPost]
        public IActionResult SubmitNote(Note data)
        {
            if (ModelState.IsValid)
            {
                context.Add(data);
                context.SaveChanges();

                return RedirectToAction("ManageNotes");
            }

            return RedirectToAction("ErrorBadRequest", "Error");
        }
    }
}