using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Portfolio.Database;
using Storage.Models;
using System;

namespace Storage.Services
{
    public class ImagesService : IImagesService
    {
        private readonly ApplicationDbContext _context;
        private readonly string imagesFolderName = Path.Combine("Resources", "Images");

        public ImagesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Image?> UploadAsync(IFormFile file)
        {
            if (file.Length <= 0) return null;

            string fileUniqueName =
                string.Concat(
                    Guid.NewGuid().ToString(),
                    file.FileName.AsSpan(file.FileName.LastIndexOf(".")));
            string fullPath = Path.Combine(imagesFolderName, fileUniqueName);

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

            string imgPath = Path.Combine(imagesFolderName, image.UniqueName);

            if (File.Exists(imgPath))
                File.Delete(imgPath);
            else
                return false;

            _context.Images.Remove(image);

            return true;
        }
    }
}

