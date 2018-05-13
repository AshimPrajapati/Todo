using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Todo1.Models
{
    public partial class TodoContext : DbContext
    {
        public virtual DbSet<Todolist> Todolist { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(@"Server=DESKTOP\SQLEXPRESS;Database=Todo;Trusted_Connection=True;");
        //    }
        //}
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todolist>(entity =>
            {
                entity.ToTable("todolist");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });
        }
    }
}
