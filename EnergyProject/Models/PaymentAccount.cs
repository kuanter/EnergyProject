namespace EnergyProject.Models
{
    public class PaymentAccount
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string AddressId { get; set; }
        public string TariffId { get; set; }
        public string? MeterId { get; set; }

        public string PowerStatusId { get; set; }

        public User User { get; set; }
        public Address Address { get; set; }
        public Tariff Tariff { get; set; }
        public Meter? Meter { get; set; }
        public PowerStatus PowerStatus { get; set; }
        public ICollection<Bill> Bills { get; set; }
        
    }
}
