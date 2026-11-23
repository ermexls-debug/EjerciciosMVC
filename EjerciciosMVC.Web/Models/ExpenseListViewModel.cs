using EjerciciosMVC.Web.Data.Entities;
using System.Globalization;

namespace EjerciciosMVC.Web.Models
{
    public class ExpenseListViewModel
    {
        public DateTime MonthStart { get; set; }

        public List<Expense> Expenses { get; set; } = new List<Expense>();

        public decimal Total { get; set; }

        public string MonthName
        {
            get { return MonthStart.ToString("MMMM 'de' yyyy", new CultureInfo("es-CO")); }
        }

        public DateTime PreviousMonth
        {
            get { return MonthStart.AddMonths(-1); }
        }

        public DateTime NextMonth
        {
            get { return MonthStart.AddMonths(1); }
        }
    }
}
