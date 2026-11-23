using EjerciciosMVC.Web.Data;
using EjerciciosMVC.Web.Data.Entities;
using EjerciciosMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EjerciciosMVC.Web.Controllers
{
    public class NoteCategoriesController : Controller
    {
        private readonly DataContext _context;

        public NoteCategoriesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            NoteCategoryListViewModel model = new NoteCategoryListViewModel();
            model.Categories = await GetCategoriesAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NoteCategoryListViewModel model)
        {
            if (ModelState.IsValid)
            {
                string name = model.NewName!.Trim();
                bool exists = await _context.NoteCategories.AnyAsync(c => c.Name == name);

                if (exists)
                {
                    ModelState.AddModelError(nameof(model.NewName), "Ya existe una categoría con ese nombre");
                }
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategoriesAsync();
                return View("Index", model);
            }

            NoteCategory category = new NoteCategory
            {
                Name = model.NewName!.Trim()
            };

            _context.NoteCategories.Add(category);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Categoría creada";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            NoteCategory? category = await _context.NoteCategories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            bool hasNotes = await _context.Notes.AnyAsync(n => n.CategoryId == id);

            if (hasNotes)
            {
                TempData["Error"] = "No se puede eliminar la categoría porque tiene notas";
                return RedirectToAction(nameof(Index));
            }

            _context.NoteCategories.Remove(category);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Categoría eliminada";
            return RedirectToAction(nameof(Index));
        }

        private async Task<List<NoteCategoryItemViewModel>> GetCategoriesAsync()
        {
            return await _context.NoteCategories
                .OrderBy(c => c.Name)
                .Select(c => new NoteCategoryItemViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    NoteCount = c.Notes.Count
                })
                .ToListAsync();
        }
    }
}
