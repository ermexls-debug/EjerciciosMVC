using EjerciciosMVC.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace EjerciciosMVC.Web.Models
{
    public class MemoryScoreFormViewModel
    {
        public GameLevel Level { get; set; }

        [Range(1, 1000)]
        public int Moves { get; set; }

        [Range(1, 3600)]
        public int Seconds { get; set; }

        [Required(ErrorMessage = "Escriba su nombre")]
        [MaxLength(30, ErrorMessage = "El nombre no puede tener más de 30 caracteres")]
        public string? PlayerName { get; set; }
    }
}
