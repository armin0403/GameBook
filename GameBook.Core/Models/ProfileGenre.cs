namespace GameBook.Core.Models
{
    public class ProfileGenre
    {
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
