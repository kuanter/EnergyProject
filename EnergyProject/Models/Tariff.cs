namespace EnergyProject.Models
{
    public class Tariff
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public float PricePerKWh { get; set; }

        public ICollection<PaymentAccount> PaymentAccounts { get; set; }
    }
}
