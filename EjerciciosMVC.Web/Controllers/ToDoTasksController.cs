using Microsoft.AspNetCore.Mvc;
using EjerciciosMVC.Web.Data.Entities;
using EjerciciosMVC.Web.Models;
using EjerciciosMVC.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace EjerciciosMVC.Web.Controllers
{
    public class ToDoTasksController : Controller
    {

        private readonly DataContext _context;

        public ToDoTasksController (DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        { 
            ToDoTaskListViewModel model = new ToDoTaskListViewModel();
            model.Tasks = await _context.TodoTasks.OrderByDescending (t => t.CreatedAt).ToListAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (ToDoTaskListViewModel model)
        {
            if (!ModelState.IsValid) 
            {
                model.Tasks = await _context.TodoTasks.OrderByDescending(t => t.CreatedAt).ToListAsync();
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