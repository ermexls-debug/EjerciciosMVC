using System.ComponentModel.DataAnnotations;

namespace EjerciciosMVC.Web.Models
{
    public class TipViewModel
    {
        [Required (ErrorMessage ="Escribe el valor de la cuenta")]
        [Range (1, 100000000, ErrorMessage ="La cuenta debe estar entre 1 y 100.000.000")]
        [Display (Name = "Valor de la cuenta")]
        public decimal? BillAmount { get; set; }

        [Required(ErrorMessage = "Escriba el porcentaje de la propina")]
        [Range(0, 100, ErrorMessage = "El porccentaje debe estar entre 0 y 100")]
        [Display(Name = "Porcentaje de propina")]
        public int? Percentage { get; set; }
        
        public decimal Tip { get; set; }
        public decimal Total { get; set; } 
        public bool Calculated { get; set; }
        public void Calculate()
        {
            Tip = BillAmount.Value * Percentage.Value / 100;
            Total = BillAmount.Value + Tip;
            Calculated = true;
        }
    }
}
