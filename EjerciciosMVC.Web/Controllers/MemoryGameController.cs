using EjerciciosMVC.Web.Data;
using EjerciciosMVC.Web.Data.Entities;
using EjerciciosMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EjerciciosMVC.Web.Controllers
{
    public class MemoryGameController : Controller
    {
        private static readonly string[] Symbols = { "🍎", "🍌", "🍇", "🍓", "🍒", "🍍", "🥝", "🍉", "🥑", "🥕", "🌽", "🍋" };

        private readonly DataContext _context;

        public MemoryGameController(DataContext context)
        {
            _context = context;
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveScore(MemoryScoreFormViewModel model)
        {
            if (!Enum.IsDefined(model.Level) || model.Moves < GetPairCount(model.Level))
            {
                ModelState.AddModelError(nameof(model.Moves), "Datos de la partida no válidos");
            }

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "No se pudo guardar el puntaje. Revise su nombre e inténtelo en otra partida";
                return RedirectToAction(nameof(Index), new { level = model.Level });
            }

            GameScore score = new GameScore
            {
                PlayerName = model.PlayerName!.Trim(),
                Level = model.Level,
                Moves = model.Moves,
                Seconds = model.Seconds,
                Points = CalculatePoints(model.Level, model.Moves, model.Seconds),
                PlayedAt = DateTime.Now
            };

            _context.GameScores.Add(score);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Puntaje guardado: " + score.Points + " puntos";
            return RedirectToAction(nameof(Scores), new { level = model.Level });
        }

        [HttpGet]
        public async Task<IActionResult> Scores(GameLevel level = GameLevel.Easy)
        {
            if (!Enum.IsDefined(level))
            {
                level = GameLevel.Easy;
            }

            MemoryScoresViewModel model = new MemoryScoresViewModel
            {
                Level = level,
                Scores = await _context.GameScores
                    .Where(s => s.Level == level)
                    .OrderByDescending(s => s.Points)
                    .ThenBy(s => s.Seconds)
                    .Take(10)
                    .ToListAsync()
            };

            return View(model);
        }

        public static int CalculatePoints(GameLevel level, int moves, int seconds)
        {
            int pairCount = GetPairCount(level);
            int points = pairCount * 100 - (moves - pairCount) * 10 - seconds;
            return Math.Max(0, points) * (int)level;
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
