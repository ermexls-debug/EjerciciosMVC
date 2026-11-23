using EjerciciosMVC.Web.Data;
using EjerciciosMVC.Web.Data.Entities;
using EjerciciosMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EjerciciosMVC.Web.Controllers
{
    public class NotesController : Controller
    {
        private readonly DataContext _context;

        public NotesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search, int? categoryId)
        {
            IQueryable<Note> query = _context.Notes.Include(n => n.Category);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(n => n.Title.Contains(search) || n.Content.Contains(search));
            }

            if (categoryId != null)
            {
                query = query.Where(n => n.CategoryId == categoryId);
            }

            NoteListViewModel model = new NoteListViewModel
            {
                Search = search,
                CategoryId = categoryId,
                Notes = await query.OrderByDescending(n => n.UpdatedAt).ToListAsync(),
                Categories = await GetCategoriesAsync()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            NoteFormViewModel model = new NoteFormViewModel();
            model.Categories = await GetCategoriesAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NoteFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategoriesAsync();
                return View(model);
            }

            Note note = new Note
            {
                Title = model.Title!,
                Content = model.Content!,
                CategoryId = model.CategoryId!.Value,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Nota guardada";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Note? note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            NoteFormViewModel model = new NoteFormViewModel
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content,
                CategoryId = note.CategoryId,
                Categories = await GetCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NoteFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategoriesAsync();
                return View(model);
            }

            Note? note = await _context.Notes.FindAsync(model.Id);

            if (note == null)
            {
                return NotFound();
            }

            note.Title = model.Title!;
            note.Content = model.Content!;
            note.CategoryId = model.CategoryId!.Value;
            note.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Nota actualizada";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Note? note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Nota eliminada";
            return RedirectToAction(nameof(Index));
        }

        private async Task<List<SelectListItem>> GetCategoriesAsync()
        {
            return await _context.NoteCategories
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
