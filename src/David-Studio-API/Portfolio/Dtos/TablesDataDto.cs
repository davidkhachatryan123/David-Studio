using System;
namespace Portfolio.Dtos
{
    public class TablesDataDto<T>
    {
        public IEnumerable<T>? Entities { get; set; }
        public int TotalCount { get; set; }
    }
}

