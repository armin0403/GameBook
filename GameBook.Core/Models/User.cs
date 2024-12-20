namespace GameBook.Core.Models
{
    public class User
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
    }
}
