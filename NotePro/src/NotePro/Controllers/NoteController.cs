using Microsoft.AspNetCore.Mvc;
using NotePro.Models;
using NotePro.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace NotePro.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private AppDbContext context;
        private bool mShowFinished = false;
        private const string mActiveButton = "btn btn-success active";
        private const string mButton = "btn btn-success";

        public NoteController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult NewNote()
        {
            ViewData["Title"] = "Neue Notiz erstellen";

            return View();
        }

        public IActionResult ManageNotes(bool showFinished, string sortOrder)
        {
            ViewData["Title"] = "Notizen Verwalten";

            ViewBag.SortParam = String.IsNullOrEmpty(sortOrder) ? "sortDueDate" : "";
            ViewBag.ShowFinished = !showFinished;
            List<Note> tempList = null;
            ViewBag.ShowFinishedButton = mButton;
            if (!showFinished)
            {
                tempList = context.Notes.Where(x => x.FinishDate == null).ToList();
            }
            else
            {
                tempList = context.Notes.Where(x => x.FinishDate != null).ToList();
                ViewBag.ShowFinishedButton = mActiveButton;
            }

            ViewBag.SortImportanceButton = mButton;
            ViewBag.SortCreateDateButton = mButton;
            ViewBag.SortDueDateButton = mButton;
            switch (sortOrder)
            {
                case ("sortImportance"):
                    tempList = tempList.OrderBy(x => x.Importance).ToList();
                    ViewBag.SortImportanceButton = mActiveButton;
                    break;
                case ("sortCreateDate"):
                    tempList = tempList.OrderBy(x => x.CreateDate).ToList();
                    ViewBag.SortCreateDateButton = mActiveButton;
                    break;
                default: //sortDueDate
                    tempList = tempList.OrderBy(x => x.DueDate).ToList();
                    ViewBag.SortDueDateButton = mActiveButton;
                    break;
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
                    data.Id = context.Notes.OrderBy(x=>x.Id).Last().Id + 1;
                    data.CreateDate = DateTime.Now;
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

        [HttpPost]
        public IActionResult Checkbox(long id, string showFinished, string sortParam)
        {
            Note note = context.Notes.Where(x => x.Id == id).FirstOrDefault();
            if (note.Finished == true)
            {
                note.Finished = false;
                note.FinishDate = null;
            }
            else
            {
                note.Finished = true;
                note.FinishDate = DateTime.Now;
            }
            context.Update(note);
            context.SaveChanges();

            return ManageNotes(!Boolean.Parse(showFinished), sortParam);
        }
    }
}