namespace GameBook.Core.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Trophies { get; set; }
        public int MaxTrophies { get; set; }
        public int Progression { get
            {
                return MaxTrophies > 0 ? (int)Math.Round(100.0 * Trophies / MaxTrophies) : 0;
            }}
    }
}
