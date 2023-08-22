using Messanger.Database;
using Messanger.Models;
using Microsoft.EntityFrameworkCore;

namespace Messanger.Services
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly ApplicationDbContext _context;

        public MessagesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Message> NewMessageAsync(Message message)
        {
            await _context.Messages.AddAsync(message);

            return message;
        }
    }
}
