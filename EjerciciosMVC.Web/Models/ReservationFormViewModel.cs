using System.ComponentModel.DataAnnotations;

namespace EjerciciosMVC.Web.Models
{
    public class ReservationFormViewModel
    {
        public int ServiceId { get; set; }

        public DateTime StartAt { get; set; }

        public string? ServiceName { get; set; }

        public int DurationMinutes { get; set; }

        public decimal Price { get; set; }

        [Required(ErrorMessage = "Escriba su nombre")]
        [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        [Display(Name = "Nombre completo")]
        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "Escriba su teléfono")]
        [RegularExpression(@"^\d{7,10}$", ErrorMessage = "Escriba un teléfono válido, solo números (7 a 10 dígitos)")]
        [Display(Name = "Teléfono")]
        public string? CustomerPhone { get; set; }
    }
}
