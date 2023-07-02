namespace Storage.Services
{
    public interface IFileManagement
    {
        Task<string?> UploadImageAsync(IFormFile file);
    }
}

