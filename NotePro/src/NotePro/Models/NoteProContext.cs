using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NotePro.Models
{
    public class NoteProContext : DbContext
    {
        public NoteProContext(DbContextOptions<NoteProContext> options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
