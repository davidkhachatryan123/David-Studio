
using Nest;

namespace Search.Models
{
    public class Tag : ProjectBase
    {
        [Text]
        public string Name { get; set; } = null!;

        [Keyword]
        public string Color { get; set; } = null!;
    }
}
