using System.ComponentModel.DataAnnotations;

namespace EjerciciosMVC.Web.Data.Entities
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(2000)]
        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int CategoryId { get; set; }

        public NoteCategory? Category { get; set; }
    }
}
