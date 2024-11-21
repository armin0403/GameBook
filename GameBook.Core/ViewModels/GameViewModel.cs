namespace GameBook.Core.ViewModels
{
    public class GameViewModel
    {
        public string Name { get; set; }
        public int Trophies { get; set; }
        public int MaxTrophies { get; set; }
        public int Progression
        {
            get
            {
                return MaxTrophies > 0 ? (int)Math.Round(100.0 * Trophies / MaxTrophies) : 0;
            }
        }
    }
}
