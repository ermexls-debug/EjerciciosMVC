using EjerciciosMVC.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace EjerciciosMVC.Web.Models
{
    public class ReservationSearchViewModel
    {
        [Display(Name = "Servicio")]
        public int? ServiceId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        public DateTime? Date { get; set; }

        public Service? SelectedService { get; set; }

        public List<DateTime> AvailableSlots { get; set; } = new List<DateTime>();

        public List<SelectListItem> Services { get; set; } = new List<SelectListItem>();

        public string? Message { get; set; }

        public string DateText
        {
            get { return Date == null ? "" : Date.Value.ToString("dddd d 'de' MMMM", new CultureInfo("es-CO")); }
        }
    }
}
