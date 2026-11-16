using EjerciciosMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EjerciciosMVC.Web.Controllers
{
    public class TipsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            TipViewModel model = new TipViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(TipViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Calculate();
            return View(model);
        }

    }
}
