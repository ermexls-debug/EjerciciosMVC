using EjerciciosMVC.Web.Data.Entities;

namespace EjerciciosMVC.Web.Models
{
    public class MemoryScoresViewModel
    {
        public GameLevel Level { get; set; }

        public List<GameScore> Scores { get; set; } = new List<GameScore>();

        public string LevelName
        {
            get { return MemoryGameViewModel.GetLevelName(Level); }
        }
    }
}
