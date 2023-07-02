using Microsoft.AspNetCore.Mvc;
using Storage.Models;
using Storage.Services;

namespace Storage.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class Images : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public Images(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [MapToApiVersion("1.0")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            Image? image = await _repositoryManager.Files.UploadImageAsync(file);
            await _repositoryManager.SaveAsync();

            return image is null
                ? StatusCode(500)
                : StatusCode(201, image);
        }

        [MapToApiVersion("1.0")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await _repositoryManager.Files.DeleteImageAsync(id);
            await _repositoryManager.SaveAsync();

            return !isDeleted
                ? NotFound()
                : Ok();
        }
    }
}

