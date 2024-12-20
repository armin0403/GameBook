using GameBook.Core.Models;

namespace GameBook.Services.Services
{
    public interface IUserService
    {
        Task<User> GetByUsernameOrEmail (string usernameOrEmail);
        Task<bool> AddNewUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<User> DeleteUserAsync(User user);
    }
}
