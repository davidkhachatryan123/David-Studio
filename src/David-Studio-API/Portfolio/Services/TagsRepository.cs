using AutoMapper;
using Portfolio.Database;

namespace Portfolio.Services
{
    public class TagsRepository : ITagsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TagsRepository(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}

