using GameBook.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameBook.Web.ViewModels
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Difficulty { get; set; }
        public int Rating { get; set; }
        public IFormFile PhotoUpload { get; set; }
        public string PhotoPath { get; set; }
        public int Trophies { get; set; }
        public int MaxTrophies { get; set; }
        public int Progression
        {
            get
            {
                return MaxTrophies > 0 ? (int)Math.Round(100.0 * Trophies / MaxTrophies) : 0;
            }
        }

        public IEnumerable<SelectListItem> GamePlatforms { get; set; }
        public IEnumerable<SelectListItem> GameGenres { get; set; }

        public List<int> SelectedGamePlatforms { get; set; }
        public List<int> SelectedGameGenres { get; set; }
    }
}
