using Microsoft.AspNetCore.Mvc;
using NotePro.Models;
using NotePro.Data;
using System.Linq;
using System.Collections.Generic;

namespace NotePro.Controllers
{
    public class NoteController : Controller
    {
        private NoteProContext context;
        private bool mShowFinished = false;

        public NoteController(NoteProContext context)
        {
            this.context = context;
        }

        public IActionResult NewNote()
        {
            ViewData["Title"] = "Neue Notiz erstellen";

            return View();
        }

        public IActionResult ManageNotes()
        {
            ViewData["Title"] = "Notizen Verwalten";


            List<Note> tempList = null;
            if(!mShowFinished)
            {
                tempList = context.Notes.Where(x => x.FinishDate == null).ToList();
            }
            else
            {
                tempList = context.Notes.Where(x => x.FinishDate != null).ToList();
            }
            NoteList list = new NoteList(tempList);

            return View("ManageNotes", list);
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
                if(data.Id >= 0)
                {
                    context.Update(data);
                }
                else
                {
                    data.Id = context.Notes.Last().Id + 1; //TODO: unsave
                    context.Add(data);
                }
                context.SaveChanges();

                return RedirectToAction("ManageNotes");
            }

            return RedirectToAction("ErrorBadRequest", "Error");
        }

        public IActionResult ShowFinished()
        {
            mShowFinished = !mShowFinished;
            return RedirectToAction("ManageNotes"); //TODO: redirect to previous page
        }

        public IActionResult Edit(long id)
        {
            Note note = context.Notes.Where(x => x.Id == id).FirstOrDefault();
            ViewData["Title"] = "Notiz editieren";
            return View("NewNote", note);
        }
    }
}