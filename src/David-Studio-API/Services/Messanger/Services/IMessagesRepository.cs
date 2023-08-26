using Messanger.Dtos;
using Messanger.Models;
using Services.Common.Models;

namespace Messanger.Services
{
    public interface IMessagesRepository
    {
        Task<Message> NewMessageAsync(Message message);
        Task<PageData<Message>> GetMessagesListAsync(PageOptions options);
        Task<Message?> ReadMessageAsync(int id);
        Task<Answer?> ReadAnswerAsync(int messageId);
        Task<Answer> AnswerAsync(int id, string body);
    }
}
