using System.ComponentModel.DataAnnotations;

namespace Portfolio.Dtos
{
    public class TableOptionsDto
    {
        [Required(ErrorMessage = "Page is required")]
        public int Page { get; set; }

        [Required(ErrorMessage = "PageSize is required")]
        public int PageSize { get; set; }

        [Required(ErrorMessage = "OrderBy is required")]
        public string OrderBy { get; set; } = null!;
    }
}

