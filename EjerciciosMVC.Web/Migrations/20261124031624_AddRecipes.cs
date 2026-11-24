using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EjerciciosMVC.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MealType = table.Column<int>(type: "int", nullable: false),
                    Ingredients = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    PrepMinutes = table.Column<int>(type: "int", nullable: false),
                    Servings = table.Column<int>(type: "int", nullable: false),
                    IsFavorite = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CreatedAt", "Ingredients", "Instructions", "IsFavorite", "MealType", "Name", "PrepMinutes", "Servings" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2 tazas de harina de maíz precocida\n2 tazas de agua tibia\n1 taza de queso rallado\nSal al gusto\nMantequilla", "Mezclar la harina con el agua y la sal hasta formar una masa suave.\nAgregar el queso y amasar.\nFormar las arepas y asarlas a fuego medio 5 minutos por lado.\nServir con mantequilla.", true, 1, "Arepas con queso", 25, 4 },
                    { 2, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2 tazas de arroz\n1 pechuga de pollo desmechada\n1 zanahoria picada\n1 taza de arveja\n1 pimentón\n1 cebolla\n4 tazas de caldo de pollo", "Sofreír la cebolla y el pimentón.\nAgregar el arroz y revolver 2 minutos.\nAgregar el caldo, la zanahoria y la arveja.\nCuando seque, agregar el pollo, tapar y cocinar a fuego bajo 15 minutos.", false, 2, "Arroz con pollo", 50, 5 },
                    { 3, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1 papaya\n2 bananos\n1 mango\n1 taza de fresas\nCrema de leche\nQueso rallado", "Picar todas las frutas en cubos.\nMezclar en un recipiente.\nServir con crema de leche y queso rallado por encima.", false, 4, "Ensalada de frutas", 15, 4 },
                    { 4, new DateTime(2026, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "4 huevos\n1 tomate picado\n2 tallos de cebolla larga\nSal\nAceite", "Sofreír la cebolla y el tomate en aceite.\nAgregar los huevos batidos con sal.\nRevolver hasta que estén cocidos.", true, 3, "Huevos pericos", 10, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_Name",
                table: "Recipes",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
