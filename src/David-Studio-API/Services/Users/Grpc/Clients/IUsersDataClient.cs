using Services.Common.Models;
using Users.Dtos;

namespace Users.Grpc.Clients
{
    public interface IUsersDataClient
    {
        Task<PageData<UserReadDto>> GetAllAsync(PageOptions options);
        Task<UserReadDto?> GetByIdAsync(string id);
        Task<UserReadDto?> CreateAsync(UserCreateDto user);
        Task<UserReadDto?> UpdateAsync(string id, UserCreateDto user);
        Task<UserReadDto?> DeleteAsync(string id);
    }
}
