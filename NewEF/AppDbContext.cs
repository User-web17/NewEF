using Microsoft.EntityFrameworkCore;
using NewEF.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewEF
{
    public class AppDbContext : DbContext
    {
        private readonly string _connectionString = "Data Source=NewEF.db";

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}
