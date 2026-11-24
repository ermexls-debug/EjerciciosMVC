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

        public DbSet<GameScore> GameScores { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureNotes(modelBuilder);
            ConfigureExpenses(modelBuilder);
            ConfigureReservations(modelBuilder);
            ConfigureRecipes(modelBuilder);
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

        private void ConfigureRecipes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasIndex(r => r.Name);

            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    Id = 1,
                    Name = "Arepas con queso",
                    MealType = MealType.Breakfast,
                    Ingredients = "2 tazas de harina de maíz precocida\n2 tazas de agua tibia\n1 taza de queso rallado\nSal al gusto\nMantequilla",
                    Instructions = "Mezclar la harina con el agua y la sal hasta formar una masa suave.\nAgregar el queso y amasar.\nFormar las arepas y asarlas a fuego medio 5 minutos por lado.\nServir con mantequilla.",
                    PrepMinutes = 25,
                    Servings = 4,
                    IsFavorite = true,
                    CreatedAt = new DateTime(2026, 11, 1)
                },
                new Recipe
                {
                    Id = 2,
                    Name = "Arroz con pollo",
                    MealType = MealType.Lunch,
                    Ingredients = "2 tazas de arroz\n1 pechuga de pollo desmechada\n1 zanahoria picada\n1 taza de arveja\n1 pimentón\n1 cebolla\n4 tazas de caldo de pollo",
                    Instructions = "Sofreír la cebolla y el pimentón.\nAgregar el arroz y revolver 2 minutos.\nAgregar el caldo, la zanahoria y la arveja.\nCuando seque, agregar el pollo, tapar y cocinar a fuego bajo 15 minutos.",
                    PrepMinutes = 50,
                    Servings = 5,
                    IsFavorite = false,
                    CreatedAt = new DateTime(2026, 11, 1)
                },
                new Recipe
                {
                    Id = 3,
                    Name = "Ensalada de frutas",
                    MealType = MealType.Dessert,
                    Ingredients = "1 papaya\n2 bananos\n1 mango\n1 taza de fresas\nCrema de leche\nQueso rallado",
                    Instructions = "Picar todas las frutas en cubos.\nMezclar en un recipiente.\nServir con crema de leche y queso rallado por encima.",
                    PrepMinutes = 15,
                    Servings = 4,
                    IsFavorite = false,
                    CreatedAt = new DateTime(2026, 11, 1)
                },
                new Recipe
                {
                    Id = 4,
                    Name = "Huevos pericos",
                    MealType = MealType.Dinner,
                    Ingredients = "4 huevos\n1 tomate picado\n2 tallos de cebolla larga\nSal\nAceite",
                    Instructions = "Sofreír la cebolla y el tomate en aceite.\nAgregar los huevos batidos con sal.\nRevolver hasta que estén cocidos.",
                    PrepMinutes = 10,
                    Servings = 2,
                    IsFavorite = true,
                    CreatedAt = new DateTime(2026, 11, 1)
                });
        }
    }
}
