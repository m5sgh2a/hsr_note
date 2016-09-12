using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotePro.Models;

namespace NotePro.Data
{
    public class NoteProContext : DbContext
    {
        public NoteProContext(DbContextOptions<NoteProContext> options) : base(options)
        {

        }

        public DbSet<AuthorNew> AuthorsNew { get; set; }
        public DbSet<AuthorLogin> AuthorsLogin { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
