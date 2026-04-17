namespace EnergyProject.Models
{
    public class PowerStatus
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<PaymentAccount> PaymentAccounts { get; set; }

        public PowerStatus()
        {
            PaymentAccounts = new List<PaymentAccount>();
        }

        public PowerStatus(string status, string reason)
        {
            Id = Guid.NewGuid().ToString();
            UpdatedAt = DateTime.Now; 
            Status = status;
            Reason = reason;
            PaymentAccounts = new List<PaymentAccount>();
        }

    }
}
