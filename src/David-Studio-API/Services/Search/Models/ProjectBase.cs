using Nest;

namespace Search.Models
{
    public abstract class ProjectBase
    {
        public int Id { get; set; }
        public JoinField Join { get; set; } = null!;
    }
}
