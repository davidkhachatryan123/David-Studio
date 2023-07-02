using Storage.Models;

namespace Storage.Services
{
    public interface IFileManagement
    {
        Task<Image?> UploadImageAsync(IFormFile file);
        Task<bool> DeleteImageAsync(int id);
    }
}

