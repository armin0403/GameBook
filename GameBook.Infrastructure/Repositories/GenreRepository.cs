using GameBook.Core.Models;

namespace GameBook.Infrastructure.Repositories
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(GameBookDbContext dbContext) : base(dbContext)
        {

        }
    }
}
