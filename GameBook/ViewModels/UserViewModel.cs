using GameBook.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameBook.Web.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string PhotoPath { get; set; }
        public IEnumerable<SelectListItem> CountryDropdown { get; set; }

    }
}
