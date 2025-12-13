namespace EnergyProject.Models
{
    public class CardData
    {
        public string Id { get; set; }
        public long CardNumber { get; set; }
        public string ExpMonth { get; set; } 
        public string ExpYear { get; set; }
        public string CardName { get; set; }
        public bool IsDefault { get; set; }
        public string BillId { get; set; }
        public Bill Bill { get; set; }
        public string AddressId { get; set; }
        public Address Address { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
