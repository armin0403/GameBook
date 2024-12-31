namespace GameBook.Core.Models
{
    public class Platform
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<GamePlatform> GamePlatforms { get; set; }
        public ICollection<ProfilePlatform> ProfilePlatforms { get; set; }
    }
}
