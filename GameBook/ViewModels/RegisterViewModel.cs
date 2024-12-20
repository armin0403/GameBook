using GameBook.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameBook.Web.ViewModels
{
    public class RegisterViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RetypePassword { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public string PhotoPath { get; set; }
        public IFormFile PhotoUpload { get; set; }
        public IEnumerable<SelectListItem> CountryDropdown { get; set; }
    }
}
    