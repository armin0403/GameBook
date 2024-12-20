using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameBook.Helpers.DropdownHelper
{
    public interface IDropdownService
    {
        Task<IEnumerable<SelectListItem>> GetCountryDropdownList(string searchTerm);
    }
}
