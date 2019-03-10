using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTful.Models
{
    public class BoardGameContext : DbContext
    {
        public BoardGameContext(DbContextOptions<BoardGameContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<BoardGame> BoardGames { get; set; }
    }
}
