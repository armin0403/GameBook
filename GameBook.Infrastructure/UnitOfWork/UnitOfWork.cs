using GameBook.Infrastructure.Repositories;

namespace GameBook.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameBookDbContext _dbContext;
        private readonly IGameRepository _gameRepository;
        public UnitOfWork(GameBookDbContext dbContext) 
        {
            _dbContext = dbContext;
            _gameRepository = new GameRepository(_dbContext);
        }

        public IGameRepository GameRepository => _gameRepository;

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
