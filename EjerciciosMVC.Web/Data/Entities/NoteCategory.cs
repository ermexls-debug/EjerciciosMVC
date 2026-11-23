using System.ComponentModel.DataAnnotations;

namespace EjerciciosMVC.Web.Data.Entities
{
    public class NoteCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
