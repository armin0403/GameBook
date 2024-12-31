using GameBook.Infrastructure.Repositories;

namespace GameBook.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameBookDbContext _dbContext;
        private readonly IUserRepository _userRepository;
        private readonly IGameRepository _gameRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IPlatformRepository _platformRepository;
        public UnitOfWork(GameBookDbContext dbContext) 
        {
            _dbContext = dbContext;
            UserRepository = _userRepository ??= new UserRepository(_dbContext);
            GameRepository = _gameRepository ??= new GameRepository(_dbContext);
            CountryRepository = _countryRepository ??= new CountryRepository(_dbContext);
            GenreRepository = _genreRepository ??= new GenreRepository(_dbContext);
            PlatformRepository = _platformRepository ??= new PlatformRepository(_dbContext);
        }
        public IUserRepository UserRepository {  get; private set; }
        public IGameRepository GameRepository {  get; private set; }
        public ICountryRepository CountryRepository {  get; private set; }
        public IGenreRepository GenreRepository { get; private set; }
        public IPlatformRepository PlatformRepository { get; private set; }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
