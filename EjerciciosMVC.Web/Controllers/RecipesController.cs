using EjerciciosMVC.Web.Data;
using EjerciciosMVC.Web.Data.Entities;
using EjerciciosMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EjerciciosMVC.Web.Controllers
{
    public class RecipesController : Controller
    {
        private readonly DataContext _context;

        public RecipesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(RecipeListViewModel filters)
        {
            IQueryable<Recipe> query = _context.Recipes;

            if (!string.IsNullOrWhiteSpace(filters.Search))
            {
                query = query.Where(r => r.Name.Contains(filters.Search));
            }

            if (filters.MealType != null)
            {
                query = query.Where(r => r.MealType == filters.MealType);
            }

            if (!string.IsNullOrWhiteSpace(filters.Ingredient))
            {
                query = query.Where(r => r.Ingredients.Contains(filters.Ingredient));
            }

            if (filters.FavoritesOnly)
            {
                query = query.Where(r => r.IsFavorite);
            }

            filters.Recipes = await query.OrderByDescending(r => r.IsFavorite).ThenBy(r => r.Name).ToListAsync();

            return View(filters);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Recipe? recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new RecipeFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecipeFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Recipe recipe = new Recipe
            {
                CreatedAt = DateTime.Now
            };

            CopyFormToRecipe(model, recipe);

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Receta guardada";
            return RedirectToAction(nameof(Details), new { id = recipe.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Recipe? recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            RecipeFormViewModel model = new RecipeFormViewModel
            {
                Id = recipe.Id,
                Name = recipe.Name,
                MealType = recipe.MealType,
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions,
                PrepMinutes = recipe.PrepMinutes,
                Servings = recipe.Servings
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RecipeFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Recipe? recipe = await _context.Recipes.FindAsync(model.Id);

            if (recipe == null)
            {
                return NotFound();
            }

            CopyFormToRecipe(model, recipe);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Receta actualizada";
            return RedirectToAction(nameof(Details), new { id = recipe.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Recipe? recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Receta eliminada";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleFavorite(int id, string? returnUrl)
        {
            Recipe? recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            recipe.IsFavorite = !recipe.IsFavorite;
            await _context.SaveChangesAsync();

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(Index));
        }

        private void CopyFormToRecipe(RecipeFormViewModel model, Recipe recipe)
        {
            recipe.Name = model.Name!.Trim();
            recipe.MealType = model.MealType!.Value;
            recipe.Ingredients = model.Ingredients!.Trim();
            recipe.Instructions = model.Instructions!.Trim();
            recipe.PrepMinutes = model.PrepMinutes!.Value;
            recipe.Servings = model.Servings!.Value;
        }
    }
}
