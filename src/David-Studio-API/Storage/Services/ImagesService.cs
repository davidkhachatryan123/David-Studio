using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Portfolio.Database;
using Storage.Models;
using Storage.Options;
using System;

namespace Storage.Services
{
    public class ImagesService : IImagesService
    {
        private readonly ApplicationDbContext _context;
        private readonly string ImagesFolderName;

        public ImagesService(
            ApplicationDbContext context,
            IOptions<StorageOptions> storageOptions)
        {
            _context = context;

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

            Image image = new()
            {
                FileName = file.FileName,
                UniqueName = fileUniqueName
            };
            await _context.Images.AddAsync(image);

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

            _context.Images.Remove(image);

            return true;
        }
    }
}

