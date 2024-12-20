using GameBook.Core.Models;

namespace GameBook.Infrastructure.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetbyUsernameOrEmailAsync(string usernameOrEmail);
    }
}
