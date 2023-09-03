using IdentityServer.Services;

namespace IdentityServer.RepositoryManager.Services
{
    public interface IRepositoryManager
    {
        IManageUsersRepository ManageUsers { get; }

        Task SaveAsync();
    }
}
