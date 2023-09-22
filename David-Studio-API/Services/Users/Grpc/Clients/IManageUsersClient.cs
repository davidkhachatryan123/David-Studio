using Users.Dtos;

namespace Users.Grpc.Clients
{
    public interface IManageUsersClient
    {
        Task<TokenResponse> GetEmailConfirmationTokenAsync(string userId);
    }
}
