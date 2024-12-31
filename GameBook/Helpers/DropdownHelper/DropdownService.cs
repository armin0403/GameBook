using System.Collections.Specialized;
using GameBook.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameBook.Helpers.DropdownHelper
{
    public class DropdownService : IDropdownService
    {
        private readonly IUnitOfWork unitOfWork;
        public DropdownService(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<SelectListItem>> GetCountryDropdownList(string? searchTerm, int? selected)
        {
            var countries = await unitOfWork.CountryRepository.GetAllAsyncE();
            
            if(string.IsNullOrEmpty(searchTerm))
            {
                countries = countries.Take(5).ToList();
            }
            else
            {
                countries = countries.Where(c => c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = selected.HasValue && c.Id == selected.Value,
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetGenreDropdownList(string searchTerm, int? selected)
        {
            var genres = await unitOfWork.GenreRepository.GetAllAsyncE();
            if(string.IsNullOrEmpty(searchTerm))
            {
                genres = genres.Take(5).ToList();
            }
            else
            {
                genres = genres.Where(g => g.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return genres.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name,
                Selected = selected.HasValue && g.Id == selected.Value,
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetPlatformSelectList()
        {
            var platforms = await unitOfWork.PlatformRepository.GetAllAsyncE();
            return platforms.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name,
            });
            
        }
    }
}
