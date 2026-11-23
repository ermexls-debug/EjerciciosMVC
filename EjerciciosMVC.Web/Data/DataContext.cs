using EjerciciosMVC.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EjerciciosMVC.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<TodoTask> TodoTasks { get; set; }

        public DbSet<NoteCategory> NoteCategories { get; set; }

        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureNotes(modelBuilder);
        }

        private void ConfigureNotes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NoteCategory>().HasIndex(c => c.Name).IsUnique();

            modelBuilder.Entity<Note>().HasOne(n => n.Category).WithMany(c => c.Notes).HasForeignKey(n => n.CategoryId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NoteCategory>().HasData(
                new NoteCategory { Id = 1, Name = "Personal" },
                new NoteCategory { Id = 2, Name = "Estudio" },
                new NoteCategory { Id = 3, Name = "Trabajo" },
                new NoteCategory { Id = 4, Name = "Ideas" });
        }
    }
}
