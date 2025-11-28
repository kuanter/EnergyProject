namespace EnergyProject.Models
{
    public class Bill
    {
        public string Id { get; set; }
        public float ConsumptionKWh { get; set; }
        public float Amount { get; set; }
        public string Status { get; set; }
        public DateTime GeneratedAt { get; set; }

        public string PaymentAccountId { get; set; }
        public string CardDataId { get; set; }

        public PaymentAccount PaymentAccount { get; set; }
        public CardData CardData { get; set; }
    }
}
