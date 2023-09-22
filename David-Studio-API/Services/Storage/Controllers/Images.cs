using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using Storage.Services;

namespace Storage.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/storage/[controller]")]
    public class Images : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILogger<Images> _logger;

        public Images(IRepositoryManager repositoryManager, ILogger<Images> logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            _logger.LogInformation("Requested for upload image: {FileName}", file.FileName);

            Image? image = await _repositoryManager.Images.UploadAsync(file);
            await _repositoryManager.SaveAsync();

            return image is null
                ? StatusCode(500)
                : StatusCode(201, image);
        }

        [MapToApiVersion("1.0")]
        [HttpDelete]
        [Route("{imageName}")]
        public async Task<IActionResult> Delete(string imageName)
        {
            _logger.LogInformation("Requested to delete image by name: {ImageName}", imageName);

            bool isDeleted = await _repositoryManager.Images.DeleteAsync(imageName);
            await _repositoryManager.SaveAsync();

            return !isDeleted
                ? NotFound()
                : Ok();
        }
    }
}

