using Storage.Models;

namespace Storage.Services
{
    public interface IImagesService
    {
        Task<Image?> UploadAsync(IFormFile file);
        Task<bool> DeleteAsync(string name);
    }
}

