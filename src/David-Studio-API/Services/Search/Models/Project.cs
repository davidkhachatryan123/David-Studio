using Nest;

namespace Search.Models
{
    public class Project : ProjectBase
    {
        [Text]
        public string Name { get; set; } = null!;

        [Keyword]
        public Uri Link { get; set; } = null!;

        [Keyword]
        public string ImageUrl { get; set; } = null!;
    }
}
