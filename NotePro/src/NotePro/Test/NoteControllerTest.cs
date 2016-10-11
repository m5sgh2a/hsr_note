using Microsoft.AspNetCore.Mvc;
using NotePro.Controllers;
using NotePro.Data;
using NotePro.Models;
using NotePro.Services;
using Xunit;

namespace NotePro.Test
{
    public class NoteControllerTest
    {
        private AppDbContext context;
        private NoteController controller;

        public NoteControllerTest()
        {
            context = new TestData().GetNewInMemoryDabaseWithTestData();
            controller = new NoteController(context, new NoteService(context));
        }

        [Fact]
        public void GetNote_NoteFound_ReturnNote()
        {
            var view = controller.Edit(1) as ViewResult;
            Assert.IsType(typeof(Note), view.ViewData.Model);
        }

        [Fact]
        public void GetNote_NoteNotFound_ReturnError()
        {
            var view = controller.Edit(0) as ViewResult;
            Assert.Null(view);
        }
    }
}
