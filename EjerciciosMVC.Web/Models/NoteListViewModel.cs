using EjerciciosMVC.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EjerciciosMVC.Web.Models
{
    public class NoteListViewModel
    {
        public string? Search { get; set; }

        public int? CategoryId { get; set; }

        public List<Note> Notes { get; set; } = new List<Note>();

        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
