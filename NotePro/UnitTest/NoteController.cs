using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotePro.Models;

namespace UnitTest
{
    [TestClass]
    public class NoteController
    {
        [TestMethod]
        public void GetNote_NoteNotFound_ThrowException()
        {
            Note note = mNoteService.GetNote(id);
        }
    }
}
