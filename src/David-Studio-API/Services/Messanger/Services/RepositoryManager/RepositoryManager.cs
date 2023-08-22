using AutoMapper;
using Messanger.Database;
using Messanger.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Messanger.Services.RepositoryManager
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;

        private readonly IEmailService _emailService;
        private readonly EmailOptions _emailOptions;

        private IMessagesRepository _messagesRespository = null!;

        public RepositoryManager(
            ApplicationDbContext context,
            IEmailService emailService,
            IOptions<EmailOptions> emailOptions)
        {
            _context = context;
            _emailService = emailService;
            _emailOptions = emailOptions.Value;
        }

        public IMessagesRepository Messages
        {
            get
            {
                _messagesRespository ??= new MessagesRepository(_context, _emailService, _emailOptions);

                return _messagesRespository;
            }
        }

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
