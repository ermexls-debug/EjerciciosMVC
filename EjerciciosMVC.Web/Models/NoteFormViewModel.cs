using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EjerciciosMVC.Web.Models
{
    public class NoteFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Escriba el título")]
        [MaxLength(100, ErrorMessage = "El título no puede tener más de 100 caracteres")]
        [Display(Name = "Título")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Escriba el contenido de la nota")]
        [MaxLength(2000, ErrorMessage = "La nota no puede tener más de 2000 caracteres")]
        [Display(Name = "Contenido")]
        public string? Content { get; set; }

        [Required(ErrorMessage = "Seleccione una categoría")]
        [Display(Name = "Categoría")]
        public int? CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
