using Services.Common.Models;
using Users.Dtos;

namespace Users.Grpc.Clients
{
    public interface IAdminsDataClient
    {
        Task<PageData<AdminReadDto>> GetAllAsync(PageOptions options);
        Task<AdminReadDto?> GetByIdAsync(string id);
        Task<AdminReadDto?> CreateAsync(AdminCreateDto user);
        Task<AdminReadDto?> UpdateAsync(string id, AdminCreateDto user);
        Task<AdminReadDto?> DeleteAsync(string id);
    }
}
