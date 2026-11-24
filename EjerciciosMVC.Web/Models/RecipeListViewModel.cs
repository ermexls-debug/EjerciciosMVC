using EjerciciosMVC.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace EjerciciosMVC.Web.Models
{
    public class RecipeListViewModel
    {
        [Display(Name = "Nombre")]
        public string? Search { get; set; }

        [Display(Name = "Tipo de comida")]
        public MealType? MealType { get; set; }

        [Display(Name = "Ingrediente")]
        public string? Ingredient { get; set; }

        [Display(Name = "Solo favoritas")]
        public bool FavoritesOnly { get; set; }

        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
