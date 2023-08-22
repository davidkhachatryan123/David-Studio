using Messanger.Models;

namespace Messanger.Services
{
    public interface IMessagesRepository
    {
        Task<Message> NewMessageAsync(Message message);
    }
}
