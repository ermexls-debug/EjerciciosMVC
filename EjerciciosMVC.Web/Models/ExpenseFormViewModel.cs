using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EjerciciosMVC.Web.Models
{
    public class ExpenseFormViewModel
    {
        [Required(ErrorMessage = "Escriba la descripción del gasto")]
        [MaxLength(150, ErrorMessage = "La descripción no puede tener más de 150 caracteres")]
        [Display(Name = "Descripción")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Escriba el valor del gasto")]
        [Range(1, 1000000000, ErrorMessage = "El valor debe estar entre 1 y 1.000.000.000")]
        [Display(Name = "Valor")]
        public decimal? Amount { get; set; }

        [Required(ErrorMessage = "Seleccione la fecha")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Seleccione una categoría")]
        [Display(Name = "Categoría")]
        public int? CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
