using Microsoft.AspNetCore.Http;

namespace GameBook.Services.Services
{
    public interface IPhotoService
    {
        Task AddPhotoAsync<TEntity>(IFormFile photo, TEntity entity, string folderPath) where TEntity : class;
        Task DeletePhotoAsync(string folderPath);
    }
}
