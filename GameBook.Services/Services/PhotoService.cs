using Microsoft.AspNetCore.Http;

namespace GameBook.Services.Services
{
    public class PhotoService : IPhotoService
    {
        public async Task AddPhotoAsync<TEntity>(IFormFile photo, TEntity entity, string folderPath) where TEntity : class
        {
            if(photo == null || photo.Length == 0)
                throw new ArgumentException("Invalid photo file!");

            string rootPath = Directory.GetCurrentDirectory();
            string uploadFolder = Path.Combine(rootPath, "wwwwroot", folderPath);

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
            string filePath = Path.Combine(uploadFolder, uniqueFileName);

            using(var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
            }

            var photoPathProperty = entity.GetType().GetProperty("PhotoPath");
            if(photoPathProperty != null)
            {
                string relativePath = Path.Combine(folderPath, uniqueFileName).Replace("\\", "/");
                photoPathProperty.SetValue(entity, "/" + relativePath);
            }
            else
            {
                throw new InvalidOperationException($"The entity of type {typeof(TEntity).Name} does not contain a 'PhotoPath' property.");
            }
        }

        public async Task DeletePhotoAsync(string photoPath)
        {
            if (string.IsNullOrWhiteSpace(photoPath))
                throw new Exception("Photo path doesn't exist");

            string rootPath = Directory.GetCurrentDirectory();
            string fullFilePath = Path.Combine(rootPath, "wwwroot", photoPath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));

            if(File.Exists(fullFilePath))
            {
                try
                {
                    File.Delete(fullFilePath);
                    await Task.CompletedTask;
                }
                catch(Exception ex)
                {
                    throw new Exception("Failed to delete!", ex);
                }
            }
            else
            {
                throw new Exception("File doesn't exist!");
            }
        }
    }
}
