using System.ComponentModel.DataAnnotations;

namespace EjerciciosMVC.Web.Data.Entities
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public MealType MealType { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Ingredients { get; set; } = null!;

        [Required]
        [MaxLength(4000)]
        public string Instructions { get; set; } = null!;

        public int PrepMinutes { get; set; }

        public int Servings { get; set; }

        public bool IsFavorite { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
