using Microsoft.EntityFrameworkCore;
using NotePro.Models;

namespace NotePro.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Register> Register { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
