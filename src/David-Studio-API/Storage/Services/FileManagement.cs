using Azure.Core;
using Microsoft.AspNetCore.Http;
using Portfolio.Database;
using Storage.Models;
using System;

namespace Storage.Services
{
    public class FileManagement : IFileManagement
    {
        private readonly ApplicationDbContext _context;

        public FileManagement(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string?> UploadImageAsync(IFormFile file)
        {
            string folderName = Path.Combine("Resources", "Images");

            if (file.Length <= 0) return null;

            string fileUniqueName =
                string.Concat(
                    Guid.NewGuid().ToString(),
                    file.FileName.AsSpan(file.FileName.LastIndexOf(".")));
            string fullPath = Path.Combine(folderName, fileUniqueName);

            using FileStream stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            await _context.Images.AddAsync(new Image()
            {
                FileName = file.FileName,
                Path = fileUniqueName
            });

            return fullPath;
        }
    }
}

