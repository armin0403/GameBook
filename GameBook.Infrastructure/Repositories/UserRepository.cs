using GameBook.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(GameBookDbContext dbContext)  : base(dbContext)
        {        
        }

        public async Task<User> GetbyUsernameOrEmailAsync(string usernameOrEmail)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);
        }
    }
}
