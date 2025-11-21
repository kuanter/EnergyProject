namespace EnergyProject.Models
{
    public class PaymentAccount
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public int TariffId { get; set; }
        public int MeterId { get; set; }

        public int PowerStatusId { get; set; }

        public User User { get; set; }
        public Address Address { get; set; }
        public Tariff Tariff { get; set; }
        public Meter Meter { get; set; }
        public PowerStatus PowerStatus { get; set; }
        public ICollection<Bill> Bills { get; set; }
        
    }
}
