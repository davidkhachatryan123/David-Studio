using Microsoft.AspNetCore.Mvc;
using Storage.Services;

namespace Storage.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class Files : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public Files(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [MapToApiVersion("1.0")]
        [HttpPost, DisableRequestSizeLimit]
        [Route("Image")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            string? path = await _repositoryManager.Files.UploadImageAsync(file);
            await _repositoryManager.SaveAsync();

            return path is null
                ? StatusCode(500)
                : StatusCode(201, path);
        }
    }
}

