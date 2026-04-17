namespace EnergyProject.Models
{
    public class Tariff
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public float PricePerKWh { get; set; }
        public ICollection<PaymentAccount> PaymentAccounts { get; set; }

        public Tariff()
        {
            PaymentAccounts = new List<PaymentAccount>();
        }

        public Tariff(string name, float pricePerKWh)
        {
            Id = Guid.NewGuid().ToString(); 
            Name = name;
            PricePerKWh = pricePerKWh;
            PaymentAccounts = new List<PaymentAccount>();
        }

    }
}
