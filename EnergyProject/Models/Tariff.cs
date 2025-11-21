namespace EnergyProject.Models
{
    public class Tariff
    {
       
        public int Id { get; set; }
        public float PricePerKWh { get; set; }

        public PaymentAccount PaymentAccount { get; set; }
    }
}
