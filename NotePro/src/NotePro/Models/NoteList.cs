using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotePro.Models
{
    public class NoteList
    {
        private readonly List<Note> mNoteList;

        public NoteList(List<Note> list)
        {
            mNoteList = list;
        }

        public List<Note> GetList()
        {
            return mNoteList;
        }
    }
}
