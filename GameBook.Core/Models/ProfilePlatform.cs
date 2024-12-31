namespace GameBook.Core.Models
{
    public class ProfilePlatform
    {
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

        public int PlatformId { get; set; }
        public Platform Platform { get; set; }
    }
}
