using GameBook.Core.Models;

namespace GameBook.Services.Services
{
    public interface IUserService
    {
        Task<User> GetByUsernameOrEmail (string usernameOrEmail);
        Task<User> GetUserByIdAsync (int id);
        Task<bool> AddNewUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
    }
}
