using IdentityServer.Dtos;

namespace IdentityServer.Services
{
    public interface IManageUsersRepository
    {
        Task<string> SendConfirmationEmailAsync(ConfirmEmailRequestDto requestDto);
        Task SendMfaCodeAsync(string email);
    }
}
