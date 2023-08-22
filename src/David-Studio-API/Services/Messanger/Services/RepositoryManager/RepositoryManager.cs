using AutoMapper;
using Messanger.Database;
using Microsoft.AspNetCore.Identity;

namespace Messanger.Services.RepositoryManager
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;

        private IMessagesRepository _messagesRespository = null!;

        public RepositoryManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public IMessagesRepository Messages
        {
            get
            {
                _messagesRespository ??= new MessagesRepository(_context);

                return _messagesRespository;
            }
        }

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
