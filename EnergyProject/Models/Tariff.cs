namespace EnergyProject.Models
{
    public class Tariff
    {
        public int TariffId { get; set; }
        public float PricePerKWh { get; set; }

        public PaymentAccount PaymentAccount { get; set; }
    }
}
