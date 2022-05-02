using Entities;
using Microsoft.EntityFrameworkCore;
using ProAgro.Entities;
using System;
using System.Collections.Generic;
using System.Text;
#nullable disable
namespace ProAgro.DAL
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
       : base(options)
        {

        }
        public virtual DbSet<Georreferencias> Georreferencias { get; set; }
    


        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) {
            if (!dbContextOptionsBuilder.IsConfigured) { 
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Georreferencias>(o =>
            {
                o.ToTable("Georreferencias");
                o.HasNoKey();
            });
            modelBuilder.Entity<Usuario>(o =>
            {
                o.ToTable("Usuario");
                o.HasNoKey();
            });
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
    