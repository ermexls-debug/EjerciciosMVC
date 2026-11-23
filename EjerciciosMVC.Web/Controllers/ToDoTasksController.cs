using Microsoft.AspNetCore.Mvc;
using EjerciciosMVC.Web.Data.Entities;
using EjerciciosMVC.Web.Models;
using EjerciciosMVC.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace EjerciciosMVC.Web.Controllers
{
    public class TodoTasksController : Controller
    {

        private readonly DataContext _context;

        public TodoTasksController (DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        { 
            TodoTaskListViewModel model = new TodoTaskListViewModel();
            model.Tasks = await _context.TodoTasks.OrderBy(t => t.IsCompleted).ThenByDescending(t => t.CreatedAt).ToListAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (TodoTaskListViewModel model)
        {
            if (!ModelState.IsValid) 
            {
                model.Tasks = await _context.TodoTasks.OrderBy(t => t.IsCompleted).ThenByDescending(t => t.CreatedAt).ToListAsync();
                return View("Index", model);
            }
            TodoTask task = new TodoTask
            {
                Title = model.NewTitle!,
                IsCompleted = false,
                CreatedAt = DateTime.Now
            };

            _context.TodoTasks.Add(task);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}