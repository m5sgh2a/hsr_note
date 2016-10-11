using System;
using Microsoft.EntityFrameworkCore;
using NotePro.Data;
using NotePro.Models;

namespace NotePro.Test
{
    public class TestData
    {
        private AppDbContext CreateInMemoryDatabase()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase();
            AppDbContext context = new AppDbContext(optionsBuilder.Options);

            return context;
        }

        public void AddTestData(AppDbContext context)
        {
            var author = new Register
            {
                FirstName = "Martin",
                LastName = "Meier",
                Email = "Martin.Meier@gmail.com",
                Password = "123456"
            };

            context.Register.Add(author);

            var note = new Note
            {
                Title = "Einkauf1",
                Description = "Milch einkaufen",
                Importance = 2,
                DueDate = new DateTime(2016, 9, 18),
                AuthorId = author.Id,
                CreateDate = new DateTime(2016, 8, 18)
            };
            var note2 = new Note
            {
                Title = "Einkauf2",
                Description = "Brot einkaufen",
                Importance = 3,
                DueDate = new DateTime(2016, 9, 19),
                AuthorId = author.Id,
                FinishDate = new DateTime(2016, 9, 19),
                Finished = true,
                CreateDate = new DateTime(2016, 8, 17)
            };
            var note3 = new Note
            {
                Title = "Einkauf3",
                Description = "Brot einkaufen",
                Importance = 1,
                DueDate = new DateTime(2016, 9, 20),
                AuthorId = author.Id,
                FinishDate = new DateTime(2016, 9, 18),
                Finished = true,
                CreateDate = new DateTime(2016, 8, 20)
            };

            context.Notes.Add(note);
            context.Notes.Add(note2);
            context.Notes.Add(note3);
            context.SaveChanges();
        }

        public AppDbContext GetNewInMemoryDabaseWithTestData()
        {
            var context = CreateInMemoryDatabase();
            AddTestData(context);

            return context;
        }
    }
}
