using System;
using NotePro.Data;
using NotePro.Services;
using Xunit;

namespace NotePro.Test
{
    public class NoteServiceTest
    {
        private AppDbContext context;
        private NoteService noteService;

        public NoteServiceTest()
        {
            context = new TestData().GetNewInMemoryDabaseWithTestData();
            noteService = new NoteService(context);
        }

        [Fact]
        public void SaveNote_Update_NewFinishDate()
        {
            DateTime finishDateTarget = new DateTime(2016, 10, 1);
            var note = noteService.GetNote(1);
            note.FinishDate = finishDateTarget;
            noteService.SaveNote(note);

            DateTime? finishDateActual = noteService.GetNote(1).FinishDate;
            Assert.NotNull(finishDateActual);
            Assert.Equal(finishDateTarget.Date, ((DateTime)finishDateActual).Date);
        }
    }
}
