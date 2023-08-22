using AutoMapper;
using Messanger.Dtos;
using Messanger.Models;
using Services.Common.Models;

namespace Messanger.Mappings
{
    public class ContactMappingProfile : Profile
    {
        public ContactMappingProfile()
        {
            CreateMap<ContactFormDto, Message>();

            CreateMap<Message, MessagesListItemDto>()
                .ForMember(dest => dest.HasAnswer, opt =>
                    opt.MapFrom(src => src.Answer != null));
            CreateMap<Message, MessageReadDto>();

            CreateMap<Answer, AnswerReadDto>();
        }
    }
}
