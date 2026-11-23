using EjerciciosMVC.Web.Data;
using EjerciciosMVC.Web.Data.Entities;
using EjerciciosMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EjerciciosMVC.Web.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly DataContext _context;

        public ExpensesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? year, int? month)
        {
            DateTime monthStart = GetMonthStart(year, month);
            DateTime monthEnd = monthStart.AddMonths(1);

            List<Expense> expenses = await _context.Expenses
                .Include(e => e.Category)
                .Where(e => e.Date >= monthStart && e.Date < monthEnd)
                .OrderByDescending(e => e.Date)
                .ThenByDescending(e => e.Id)
                .ToListAsync();

            ExpenseListViewModel model = new ExpenseListViewModel
            {
                MonthStart = monthStart,
                Expenses = expenses,
                Total = expenses.Sum(e => e.Amount)
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ExpenseFormViewModel model = new ExpenseFormViewModel
            {
                Date = DateTime.Today,
                Categories = await GetCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseFormViewModel model)
        {
            if (model.Date != null && model.Date.Value.Date > DateTime.Today)
            {
                ModelState.AddModelError(nameof(model.Date), "La fecha no puede ser futura");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategoriesAsync();
                return View(model);
            }

            Expense expense = new Expense
            {
                Description = model.Description!,
                Amount = model.Amount!.Value,
                Date = model.Date!.Value.Date,
                CategoryId = model.CategoryId!.Value
            };

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Gasto registrado";
            return RedirectToAction(nameof(Index), new { year = expense.Date.Year, month = expense.Date.Month });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Expense? expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Gasto eliminado";
            return RedirectToAction(nameof(Index), new { year = expense.Date.Year, month = expense.Date.Month });
        }

        [HttpGet]
        public async Task<IActionResult> Summary(int? year, int? month)
        {
            DateTime monthStart = GetMonthStart(year, month);
            DateTime monthEnd = monthStart.AddMonths(1);

            List<ExpenseCategoryTotalViewModel> categories = await _context.Expenses
                .Where(e => e.Date >= monthStart && e.Date < monthEnd)
                .GroupBy(e => e.Category!.Name)
                .Select(g => new ExpenseCategoryTotalViewModel
                {
                    CategoryName = g.Key,
                    Total = g.Sum(e => e.Amount),
                    Count = g.Count()
                })
                .OrderByDescending(c => c.Total)
                .ToListAsync();

            decimal total = categories.Sum(c => c.Total);

            foreach (ExpenseCategoryTotalViewModel category in categories)
            {
                category.Percentage = Math.Round(category.Total * 100 / total, 1);
            }

            int days = DateTime.DaysInMonth(monthStart.Year, monthStart.Month);

            if (monthStart.Year == DateTime.Today.Year && monthStart.Month == DateTime.Today.Month)
            {
                days = DateTime.Today.Day;
            }

            ExpenseSummaryViewModel model = new ExpenseSummaryViewModel
            {
                MonthStart = monthStart,
                Total = total,
                ExpenseCount = categories.Sum(c => c.Count),
                DailyAverage = Math.Round(total / days, 0),
                Categories = categories
            };

            return View(model);
        }

        private DateTime GetMonthStart(int? year, int? month)
        {
            DateTime today = DateTime.Today;
            int selectedYear = year ?? today.Year;
            int selectedMonth = month ?? today.Month;

            if (selectedMonth < 1 || selectedMonth > 12 || selectedYear < 2000 || selectedYear > 2100)
            {
                return new DateTime(today.Year, today.Month, 1);
            }

            return new DateTime(selectedYear, selectedMonth, 1);
        }

        private async Task<List<SelectListItem>> GetCategoriesAsync()
        {
            return await _context.ExpenseCategories
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToListAsync();
        }
    }
}
