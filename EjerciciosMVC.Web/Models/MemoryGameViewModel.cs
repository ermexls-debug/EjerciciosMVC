using EjerciciosMVC.Web.Data.Entities;

namespace EjerciciosMVC.Web.Models
{
    public class MemoryGameViewModel
    {
        public GameLevel Level { get; set; }

        public int PairCount { get; set; }

        public int Columns { get; set; }

        public List<string> Cards { get; set; } = new List<string>();

        public string LevelName
        {
            get { return GetLevelName(Level); }
        }

        public static string GetLevelName(GameLevel level)
        {
            if (level == GameLevel.Hard)
            {
                return "Difícil";
            }

            if (level == GameLevel.Medium)
            {
                return "Medio";
            }

            return "Fácil";
        }
    }
}
