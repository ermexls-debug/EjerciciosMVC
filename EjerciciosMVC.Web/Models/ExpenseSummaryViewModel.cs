using System.Globalization;

namespace EjerciciosMVC.Web.Models
{
    public class ExpenseSummaryViewModel
    {
        public DateTime MonthStart { get; set; }

        public decimal Total { get; set; }

        public int ExpenseCount { get; set; }

        public decimal DailyAverage { get; set; }

        public List<ExpenseCategoryTotalViewModel> Categories { get; set; } = new List<ExpenseCategoryTotalViewModel>();

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

    public class ExpenseCategoryTotalViewModel
    {
        public string CategoryName { get; set; } = null!;

        public decimal Total { get; set; }

        public int Count { get; set; }

        public decimal Percentage { get; set; }
    }
}
