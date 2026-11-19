using EjerciciosMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EjerciciosMVC.Web.Controllers
{
    public class PasswordGeneratorController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            PasswordGeneratorViewModel model = new PasswordGeneratorViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(PasswordGeneratorViewModel model)
        {
            if (!model.IncludeUppercase && !model.IncludeLowercase && !model.IncludeNumbers && !model.IncludeSymbols)
            {
                ModelState.AddModelError("", "Debes marcar al menos un tipo de carácter");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.Generate();
            return View(model);
            
        }
    }
}
