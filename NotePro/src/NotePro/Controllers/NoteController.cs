using Microsoft.AspNetCore.Mvc;
using NotePro.Models;
using NotePro.Data;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;
using NotePro.Services;

namespace NotePro.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private AppDbContext context;
        private const string mActiveButton = "btn btn-success active";
        private const string mButton = "btn btn-success";
        private readonly INoteService mNoteService;

        public NoteController(AppDbContext context, INoteService noteService)
        {
            this.context = context;
            mNoteService = noteService;
        }

        public IActionResult NewNote()
        {
            ViewData["Title"] = "Neue Notiz erstellen";

            return View();
        }

        public IActionResult ManageNotes(bool showFinished, string sortOrder)
        {
            ViewData["Title"] = "Notizen Verwalten";
            if (sortOrder == null)
            {
                sortOrder = "sortDueDate";
                showFinished = false;
            }

            ViewBag.SortParam = sortOrder;
            ViewBag.ShowFinished = showFinished;
            ViewBag.ShowFinishedNext = !showFinished;

            ViewBag.ShowFinishedButton = mButton;
            ViewBag.SortImportanceButton = mButton;
            ViewBag.SortCreateDateButton = mButton;
            ViewBag.SortDueDateButton = mButton;

            if (showFinished)
            {
                ViewBag.ShowFinishedButton = mActiveButton;
            }

            switch (sortOrder)
            {
                case ("sortImportance"):
                    ViewBag.SortImportanceButton = mActiveButton;
                    break;
                case ("sortCreateDate"):
                    ViewBag.SortCreateDateButton = mActiveButton;
                    break;
                default: //sortDueDate
                    ViewBag.SortDueDateButton = mActiveButton;
                    break;
            }

            List<Note> noteList = mNoteService.GetSortedNoteList(sortOrder, showFinished, 1); //Todo: author i
            return View("ManageNotes", noteList);
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
                mNoteService.UpdateNote(data);

                return RedirectToAction("ManageNotes");
            }

            return RedirectToAction("ErrorBadRequest", "Error");
        }

        public IActionResult Edit(long id)
        {
            Note note = mNoteService.GetNote(id);
            ViewData["Title"] = "Notiz editieren";
            return View("NewNote", note);
        }

        [HttpPost]
        public IActionResult Checkbox(long id, string showFinished, string sortParam)
        {
            mNoteService.ChangeFinishedState(id);

            return ManageNotes(!Boolean.Parse(showFinished), sortParam);
        }
    }
}