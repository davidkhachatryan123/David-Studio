using System.ComponentModel.DataAnnotations;

namespace Services.Common.Models
{
    public class PageOptions
    {
        [Required(ErrorMessage = "Page is required")]
        public int Page { get; set; }

        [Required(ErrorMessage = "Size is required")]
        public int Size { get; set; }

        [Required(ErrorMessage = "OrderBy is required")]
        public string OrderBy { get; set; } = null!;
    }
}
