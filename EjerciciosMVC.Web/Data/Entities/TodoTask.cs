using System.ComponentModel.DataAnnotations;

namespace EjerciciosMVC.Web.Data.Entities
{
    public class TodoTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength (200)]
        public string Title { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
