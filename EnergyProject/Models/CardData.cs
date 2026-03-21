namespace EnergyProject.Models
{
    public class CardData
    {
        public string Id { get; set; }
        public long CardNumber { get; set; }
        public int ExpMonth { get; set; } 
        public int ExpYear { get; set; }
        public string CardName { get; set; }
        public bool IsDefault { get; set; }
        public string AddressId { get; set; }
        public Address Address { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<Bill> Bills { get; set; } = new List<Bill>();

        public CardData()
        {
            Bills = new List<Bill>();
        }

        public CardData(string id, long cardNumber, int expMonth, int expYear,
            string cardName, bool isDefault, string addressId, string userId)
        {
            Id = id;
            CardNumber = cardNumber;
            ExpMonth = expMonth;
            ExpYear = expYear;
            CardName = cardName;
            IsDefault = isDefault;
            AddressId = addressId;
            UserId = userId;
            Bills = new List<Bill>();
        }

    }
}
