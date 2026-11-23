using System.ComponentModel.DataAnnotations;

namespace EjerciciosMVC.Web.Data.Entities
{
    public class GameScore
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string PlayerName { get; set; } = null!;

        public GameLevel Level { get; set; }

        public int Moves { get; set; }

        public int Seconds { get; set; }

        public int Points { get; set; }

        public DateTime PlayedAt { get; set; }
    }
}
