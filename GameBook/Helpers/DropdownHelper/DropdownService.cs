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
        public async Task<IEnumerable<SelectListItem>> GetCountryDropdownList(string? searchTerm)
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
            });
        }
    }
}
