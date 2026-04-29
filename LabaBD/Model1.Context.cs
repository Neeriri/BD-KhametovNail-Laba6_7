
using System;
using Microsoft.EntityFrameworkCore;

namespace LabaBD
{
    public partial class Laba4Entities : DbContext
    {
        public Laba4Entities()
        {
        }

        public Laba4Entities(DbContextOptions<Laba4Entities> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Campaigns> Campaigns { get; set; }
        public virtual DbSet<Channels> Channels { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Expenses> Expenses { get; set; }
        public virtual DbSet<Results> Results { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=Laba4;Trusted_Connection=True;");
            }
        }
    }
}