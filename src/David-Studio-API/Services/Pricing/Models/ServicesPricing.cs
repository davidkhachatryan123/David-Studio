using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pricing.Models
{
    public class ServicesPricing
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public double EconomPrice { get; set; }

        [Required]
        public double StandartPrice { get; set; }

        [Required]
        public double PremiumPlusPrice { get; set; }

        public DateTime ChangeDate { get; set; } = DateTime.Now;
    }
}
