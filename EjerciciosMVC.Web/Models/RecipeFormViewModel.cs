using EjerciciosMVC.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace EjerciciosMVC.Web.Models
{
    public class RecipeFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Escriba el nombre de la receta")]
        [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        [Display(Name = "Nombre")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Seleccione el tipo de comida")]
        [Display(Name = "Tipo de comida")]
        public MealType? MealType { get; set; }

        [Required(ErrorMessage = "Escriba los ingredientes")]
        [MaxLength(2000, ErrorMessage = "Los ingredientes no pueden tener más de 2000 caracteres")]
        [Display(Name = "Ingredientes (uno por línea)")]
        public string? Ingredients { get; set; }

        [Required(ErrorMessage = "Escriba la preparación")]
        [MaxLength(4000, ErrorMessage = "La preparación no puede tener más de 4000 caracteres")]
        [Display(Name = "Preparación (un paso por línea)")]
        public string? Instructions { get; set; }

        [Required(ErrorMessage = "Escriba el tiempo de preparación")]
        [Range(1, 1440, ErrorMessage = "El tiempo debe estar entre 1 y 1440 minutos")]
        [Display(Name = "Tiempo de preparación (minutos)")]
        public int? PrepMinutes { get; set; }

        [Required(ErrorMessage = "Escriba el número de porciones")]
        [Range(1, 50, ErrorMessage = "Las porciones deben estar entre 1 y 50")]
        [Display(Name = "Porciones")]
        public int? Servings { get; set; }
    }
}
