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
        }
    }
}
