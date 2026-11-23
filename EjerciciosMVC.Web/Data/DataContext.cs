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

        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureNotes(modelBuilder);
            ConfigureExpenses(modelBuilder);
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

        private void ConfigureExpenses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseCategory>().HasIndex(c => c.Name).IsUnique();

            modelBuilder.Entity<Expense>().HasOne(e => e.Category).WithMany(c => c.Expenses).HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExpenseCategory>().HasData(
                new ExpenseCategory { Id = 1, Name = "Alimentación" },
                new ExpenseCategory { Id = 2, Name = "Transporte" },
                new ExpenseCategory { Id = 3, Name = "Vivienda" },
                new ExpenseCategory { Id = 4, Name = "Servicios públicos" },
                new ExpenseCategory { Id = 5, Name = "Salud" },
                new ExpenseCategory { Id = 6, Name = "Educación" },
                new ExpenseCategory { Id = 7, Name = "Entretenimiento" },
                new ExpenseCategory { Id = 8, Name = "Otros" });
        }
    }
}
