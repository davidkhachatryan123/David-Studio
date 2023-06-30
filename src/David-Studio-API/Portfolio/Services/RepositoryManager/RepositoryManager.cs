using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Portfolio.Database;

namespace Portfolio.Services
{
    public class RepositoryManager : ITagsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        private ITagsRepository _tagsRepository;

        public RepositoryManager(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public ITagsRepository Tags
        {
            get
            {
                _tagsRepository ??= new TagsRepository(_context, _mapper);

                return _tagsRepository;
            }
        }
    }
}

