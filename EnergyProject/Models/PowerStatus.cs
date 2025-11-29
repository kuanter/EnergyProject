namespace EnergyProject.Models
{
    public class PowerStatus
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<PaymentAccount> PaymentAccounts { get; set; }
    }
}
