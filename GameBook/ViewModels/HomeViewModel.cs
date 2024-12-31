using GameBook.Core.Models;

namespace GameBook.ViewModels
{
    public class HomeViewModel
    {
        IEnumerable<User> Users { get; set; }
        IEnumerable<Game> Games { get; set; }
    }
}
