namespace GameBook.Core.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<GameGenre> GameGenres { get; set; }
        public ICollection<ProfileGenre> ProfileGenres { get; set; }
    }
}
