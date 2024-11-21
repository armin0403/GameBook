using GameBook.Core.Models;

namespace GameBook.Infrastructure.Repositories
{
    public class GameRepository :BaseRepository<Game>, IGameRepository
    {
        public GameRepository(GameBookDbContext dbContext) : base(dbContext)
        {

        }
            
        

    }
}
