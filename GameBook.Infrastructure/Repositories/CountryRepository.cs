using GameBook.Core.Models;

namespace GameBook.Infrastructure.Repositories
{
    public class CountryRepository :BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(GameBookDbContext dbContext) : base(dbContext)
        {

        }
    }
}
