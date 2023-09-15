using Nest;

namespace Search.Services.RepositoryManager
{
    public class RepositoryManager : IRepositoryManager
    {
        private IProjectsRepository _projectsRespository = null!;
        private ITagsRepository _tagsRespository = null!;

        private readonly ElasticClient _client;

        public RepositoryManager(ElasticClient client)
        {
            _client = client;
        }

        public IProjectsRepository Projects
        {
            get
            {
                _projectsRespository ??= new ProjectsRepository(_client);

                return _projectsRespository;
            }
        }

        public ITagsRepository Tags
        {
            get
            {
                _tagsRespository ??= new TagsRepository(_client);

                return _tagsRespository;
            }
        }
    }
}
