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

        public async Task<bool> DeleteUserAsync(User user)
        {
            UnitOfWork.UserRepository.Delete(user);
            await UnitOfWork.SaveChangesAsync();
            return true;           
        }

        public async Task<User> GetByUsernameOrEmail(string usernameOrEmail)
        {
            return await UnitOfWork.UserRepository.GetbyUsernameOrEmailAsync(usernameOrEmail);
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            return UnitOfWork.UserRepository.GetbyIdAsyncT(id);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            await UnitOfWork.UserRepository.UpdateAsync(user);
            await UnitOfWork.SaveChangesAsync(); 
            return true;
        }
    }
}
