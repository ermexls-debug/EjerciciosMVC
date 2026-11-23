using System.ComponentModel.DataAnnotations;

namespace EjerciciosMVC.Web.Models
{
    public class NoteCategoryListViewModel
    {
        [Required(ErrorMessage = "Escriba el nombre de la categoría")]
        [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        [Display(Name = "Nueva categoría")]
        public string? NewName { get; set; }

        public List<NoteCategoryItemViewModel> Categories { get; set; } = new List<NoteCategoryItemViewModel>();
    }

    public class NoteCategoryItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int NoteCount { get; set; }
    }
}
