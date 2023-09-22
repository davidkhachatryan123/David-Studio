using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Users.Dtos;

namespace Users.Grpc.Clients
{
    public class ManageUsersClient : IManageUsersClient, IDisposable
    {
        private readonly string? uri;

        private readonly GrpcChannel _channel;
        private readonly ManageUsers.ManageUsersClient _client;

        public ManageUsersClient(IConfiguration configuration)
        {
            uri = configuration.GetValue<string>("Services:IdentityServerGrpcUri");
            if (uri is null) throw new Exception("Services:IdentityServerGrpcUri variable is null");

            _channel = GrpcChannel.ForAddress(uri!);
            _client = new ManageUsers.ManageUsersClient(_channel);
        }

        public async Task<TokenResponse> GetEmailConfirmationTokenAsync(string userId)
            => await _client.GetEmailConfirmationTokenAsync(new User
            {
                Id = userId
            });

        public void Dispose()
        {
            _channel.Dispose();
        }
    }
}
