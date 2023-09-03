using EventBus.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Models;
using Users.Dtos;
using Users.Grpc.Clients;

namespace Users.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class Admins : ControllerBase
    {
        private readonly IAdminsDataClient _adminsData;
        private readonly ILogger<Admins> _logger;

        public Admins(
            IAdminsDataClient adminsData,
            ILogger<Admins> logger)
        {
            _adminsData = adminsData;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageOptions options)
        {
            PageData<AdminReadDto>? data = null;

            try
            {
                _logger.LogInformation("Trying to get all admins");

                data = await _adminsData.GetAllAsync(options);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get all admins function thrown exception: {Message}", ex.Message);
            }

            return data is null
                ? NotFound()
                : Ok(data);
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{id}", Name = nameof(Admins) + nameof(GetById))]
        public async Task<IActionResult> GetById(string id)
        {
            _logger.LogInformation("Get admin by id: {Id}", id);

            AdminReadDto? admin = await _adminsData.GetByIdAsync(id);

            return admin is null
                ? NotFound()
                : Ok(admin);
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AdminCreateDto adminCreateDto)
        {
            _logger.LogInformation("Create admin by username and email: {Username}, {Email}", adminCreateDto.Username, adminCreateDto.Email);

            AdminReadDto? admin = await _adminsData.CreateAsync(adminCreateDto);

            return admin is null
                ? BadRequest()
                : CreatedAtRoute(nameof(Admins) + nameof(GetById), new { id = admin.Id }, admin);
        }

        [MapToApiVersion("1.0")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] AdminCreateDto adminCreateDto)
        {
            _logger.LogInformation("Updating admin by id: {AdminId}", id);

            AdminReadDto? admin = await _adminsData.UpdateAsync(id, adminCreateDto);

            return admin is null
                ? BadRequest()
                : CreatedAtRoute(nameof(Admins) + nameof(GetById), new { id = admin.Id }, admin);
        }

        [MapToApiVersion("1.0")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Deleting admin by id: {AdminId}", id);

            AdminReadDto? admin = await _adminsData.DeleteAsync(id);

            return admin is null
                ? NotFound()
                : Ok(admin);
        }
    }
}
