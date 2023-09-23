using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Portfolio.Database;
using Storage.Models;
using Storage.Options;

namespace Storage.Services
{
    public class ImagesService : IImagesService
    {
        private readonly string ImagesFolderName;

        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ImagesService(
            ApplicationDbContext context,
            IOptions<StorageOptions> storageOptions,
            ILogger logger)
        {
            _context = context;
            _logger = logger;

            ImagesFolderName = Path.Combine(
                storageOptions.Value.StoragePath,
                storageOptions.Value.ImagesDir);
        }

        public async Task<Image?> UploadAsync(IFormFile file)
        {
            if (file.Length <= 0) return null;

            string fileUniqueName =
                string.Concat(
                    Guid.NewGuid().ToString(),
                    file.FileName.AsSpan(file.FileName.LastIndexOf(".")));
            string fullPath = Path.Combine(ImagesFolderName, fileUniqueName);

            using FileStream stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            _logger.LogInformation("Image saved to file system by path: {Path}", fullPath);

            Image image = new()
            {
                FileName = file.FileName,
                UniqueName = fileUniqueName
            };
            await _context.Images.AddAsync(image);

            _logger.LogInformation("Add image to database: {UniqueName}", image.UniqueName);

            return image;
        }

        public async Task<bool> DeleteAsync(string name)
        {
            Image? image = await _context.Images.FirstOrDefaultAsync(img => img.UniqueName == name);

            if (image is null) return false;

            string imgPath = Path.Combine(ImagesFolderName, image.UniqueName);

            if (File.Exists(imgPath))
                File.Delete(imgPath);
            else
                return false;

            _logger.LogInformation("Image deleted from file system by path: {Path}", imgPath);

            _context.Images.Remove(image);

            _logger.LogInformation("Remove image from database: {UniqueName}", image.UniqueName);

            return true;
        }
    }
}
