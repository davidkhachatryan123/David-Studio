namespace Pricing.Dtos
{
    public class ServicesPricingReadDto
    {
        public int Id { get; set; }

        public double EconomPrice { get; set; }
        public double StandartPrice { get; set; }
        public double PremiumPlusPrice { get; set; }

        public DateTime ChangeDate { get; set; }
    }
}
