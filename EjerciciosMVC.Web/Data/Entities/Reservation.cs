using System.ComponentModel.DataAnnotations;

namespace EjerciciosMVC.Web.Data.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public int ServiceId { get; set; }

        public Service? Service { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string CustomerPhone { get; set; } = null!;

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public ReservationStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
