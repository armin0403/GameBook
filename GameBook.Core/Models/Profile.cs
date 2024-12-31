namespace GameBook.Core.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string? Bio { get; set;}
        public string? Education { get; set;}
        public string? Occupation { get; set;}
        public string? FavoriteQuote { get; set;}        

        public ICollection<ProfilePlatform> FavoritePlatform { get; set; }
        public ICollection<ProfileGenre> FavoriteGenre { get; set; } 

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
