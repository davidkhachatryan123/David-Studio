using System.Runtime.InteropServices.JavaScript;
using Messanger.Database;
using Messanger.Dtos;
using Messanger.Models;
using Messanger.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Services.Common.Extensions;
using Services.Common.Models;

namespace Messanger.Services
{
    public class MessagesRepository : IMessagesRepository
    {
        private const string _subject = "Answer for your last request";

        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly EmailOptions _emailOptions;

        public MessagesRepository(
            ApplicationDbContext context,
            IEmailService emailService,
            EmailOptions emailOptions)
        {
            _context = context;
            _emailService = emailService;
            _emailOptions = emailOptions;
        }

        public async Task<Message> NewMessageAsync(Message message)
        {
            await _context.Messages.AddAsync(message);

            return message;
        }

        public async Task<PageData<Message>> GetMessagesListAsync(PageOptions options)
            => await _context.Messages
                             .Include(m => m.Answer)
                             .ToPagedAsync(options);

        public async Task<Message?> ReadMessageAsync(int id)
        {
            Message? message = await _context.Messages
                .Include(m => m.Answer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (message is null) return null;

            message.IsReaded = true;
            await _context.SaveChangesAsync();

            return message;
        }

        public async Task<Answer> AnswerAsync(int id, string body)
        {
            Message? message = await ReadMessageAsync(id);
            if (message is null) throw new Exception($"Message not found by id: {id}");
            if (message.Answer is not null) throw new Exception($"Message already has answer");

            Answer answer = new Answer
            {
                MessageId = message.Id,
                Body = body
            };

            await _context.Answers.AddAsync(answer);

            string to = message.Email;
            await _emailService.SendEmailAsync(
                _emailOptions.FromForAnswer, to,
                _subject, body,
                withPrefix: true);

            return answer;
        }
    }
}
