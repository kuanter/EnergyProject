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

        public PowerStatus(string id, string status, string reason, DateTime updatedAt)
        {
            Id = id;
            Status = status;
            Reason = reason;
            UpdatedAt = updatedAt;
            PaymentAccounts = new List<PaymentAccount>();
        }

    }
}
