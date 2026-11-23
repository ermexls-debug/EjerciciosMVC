using EjerciciosMVC.Web.Data.Entities;
using EjerciciosMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EjerciciosMVC.Web.Controllers
{
    public class MemoryGameController : Controller
    {
        private static readonly string[] Symbols = { "🍎", "🍌", "🍇", "🍓", "🍒", "🍍", "🥝", "🍉", "🥑", "🥕", "🌽", "🍋" };

        [HttpGet]
        public IActionResult Index(GameLevel level = GameLevel.Easy)
        {
            if (!Enum.IsDefined(level))
            {
                level = GameLevel.Easy;
            }

            int pairCount = GetPairCount(level);

            List<string> cards = new List<string>();

            for (int i = 0; i < pairCount; i++)
            {
                cards.Add(Symbols[i]);
                cards.Add(Symbols[i]);
            }

            string[] shuffled = cards.ToArray();
            Random.Shared.Shuffle(shuffled);

            MemoryGameViewModel model = new MemoryGameViewModel
            {
                Level = level,
                PairCount = pairCount,
                Columns = level == GameLevel.Hard ? 6 : 4,
                Cards = shuffled.ToList()
            };

            return View(model);
        }

        public static int GetPairCount(GameLevel level)
        {
            if (level == GameLevel.Hard)
            {
                return 12;
            }

            if (level == GameLevel.Medium)
            {
                return 8;
            }

            return 6;
        }
    }
}
