using NotePro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotePro.Services
{
    public interface INoteService
    {
        List<Note> GetSortedNoteList(string sortOrder, bool showFinished, long authorId);

        void UpdateNote(Note note);

        void ChangeFinishedState(long id);

        Note GetNote(long id);
    }
}
