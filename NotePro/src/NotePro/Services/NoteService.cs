using NotePro.Data;
using NotePro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotePro.Services
{
    public class NoteService : INoteService
    {
        private AppDbContext mContext;

        public NoteService(AppDbContext context)
        {
            mContext = context;
        }

        public List<Note> GetSortedNoteList(string sortOrder, bool showFinished, long authorId)
        {
            var noteList = new List<Note>();
            if (!showFinished)
            {
                noteList = mContext.Notes.Where(x => x.FinishDate == null && x.AuthorId== authorId).ToList();
            }
            else
            {
                noteList = mContext.Notes.Where(x => x.FinishDate != null && x.AuthorId == authorId).ToList();
            }

            switch (sortOrder)
            {
                case ("sortImportance"):
                    noteList = noteList.OrderBy(x => x.Importance).ToList();
                    break;
                case ("sortCreateDate"):
                    noteList = noteList.OrderBy(x => x.CreateDate).ToList();
                    break;
                default: //sortDueDate
                    noteList = noteList.OrderBy(x => x.DueDate).ToList();
                    break;
            }
            return noteList;
        }

        public void UpdateNote(Note note)
        {
            if (note.Id >= 0)
            {
                mContext.Update(note);
            }
            else if(mContext.Notes.Where(x=>x.Id==note.Id).ToList().Count==0)
            {
                //TODO: throw 404 error
            }
            else
            {
                note.Id = mContext.Notes.OrderBy(x => x.Id).Last().Id + 1;
                note.CreateDate = DateTime.Now;
                mContext.Add(note);
            }
            mContext.SaveChanges();
        }

        public void ChangeFinishedState(long id)
        {
            Note note = mContext.Notes.Where(x => x.Id == id).FirstOrDefault();
            if(note==null)
            {
                //TODO: throw 404 error
            }
            else if (note.Finished == true)
            {
                note.Finished = false;
                note.FinishDate = null;
            }
            else
            {
                note.Finished = true;
                note.FinishDate = DateTime.Now;
            }
            mContext.Update(note);
            mContext.SaveChanges();
        }

        public Note GetNote(long id)
        {
            Note note = mContext.Notes.Where(x => x.Id == id).FirstOrDefault();
            if(note==null)
            {
                //TODO: throw 404 error
            }
            return note;
        }
    }
}
