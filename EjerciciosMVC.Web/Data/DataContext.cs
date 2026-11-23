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

        public DbSet<Service> Services { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureNotes(modelBuilder);
            ConfigureExpenses(modelBuilder);
            ConfigureReservations(modelBuilder);
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

        private void ConfigureReservations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>().HasOne(r => r.Service).WithMany(s => s.Reservations).HasForeignKey(r => r.ServiceId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>().HasIndex(r => r.StartAt);

            modelBuilder.Entity<Service>().HasData(
                new Service { Id = 1, Name = "Corte de cabello", DurationMinutes = 30, Price = 25000 },
                new Service { Id = 2, Name = "Arreglo de barba", DurationMinutes = 30, Price = 15000 },
                new Service { Id = 3, Name = "Corte y barba", DurationMinutes = 60, Price = 35000 },
                new Service { Id = 4, Name = "Manicure", DurationMinutes = 60, Price = 30000 },
                new Service { Id = 5, Name = "Masaje relajante", DurationMinutes = 90, Price = 80000 });
        }
    }
}
