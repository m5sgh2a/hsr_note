using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotePro.Models;

namespace NotePro.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Register> Register { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
