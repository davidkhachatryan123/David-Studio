namespace Services.Common.Models
{
    public class PageData<T>
    {
        public IEnumerable<T>? Entities { get; set; }
        public int TotalCount { get; set; }
    }
}

