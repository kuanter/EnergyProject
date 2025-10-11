namespace EnergyProject.Models
{
    public class CardData
    {
        public int Id { get; set; }
        public long CardNumber { get; set; }
        public string ExpMonth { get; set; }
        public string ExpYear { get; set; }
        public string CardName { get; set; }
        public bool IsDefault { get; set; }
        public int BillingAddressId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
