using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameBook.Helpers.DropdownHelper
{
    public interface IDropdownService
    {
        Task<IEnumerable<SelectListItem>> GetCountryDropdownList(string searchTerm, int? selected);
        Task<IEnumerable<SelectListItem>> GetGenreDropdownList(string searchTerm, int? selected);
        Task<IEnumerable<SelectListItem>> GetPlatformSelectList();
    }
}
