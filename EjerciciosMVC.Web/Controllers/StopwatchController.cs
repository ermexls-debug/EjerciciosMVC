using Microsoft.AspNetCore.Mvc;

namespace EjerciciosMVC.Web.Controllers
{
    public class StopwatchController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
