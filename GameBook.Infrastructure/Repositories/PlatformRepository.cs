using GameBook.Core.Models;

namespace GameBook.Infrastructure.Repositories
{
    public class PlatformRepository :BaseRepository<Platform>, IPlatformRepository
    {
        public PlatformRepository(GameBookDbContext dbContext) : base(dbContext)
        {

        }
    }
}
