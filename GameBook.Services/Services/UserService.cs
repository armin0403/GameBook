using GameBook.Core.Models;
using GameBook.Infrastructure.UnitOfWork;

namespace GameBook.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork UnitOfWork;

        public UserService(IUnitOfWork unitOfWork) 
        {
            this.UnitOfWork = unitOfWork;
        }
        public async Task<bool> AddNewUserAsync(User user)
        {
            await UnitOfWork.UserRepository.AddAsync(user);
            await UnitOfWork.SaveChangesAsync();
            return true;
        }

        public Task<User> DeleteUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByUsernameOrEmail(string usernameOrEmail)
        {
            return await UnitOfWork.UserRepository.GetbyUsernameOrEmailAsync(usernameOrEmail);
        }

        public Task<User> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
