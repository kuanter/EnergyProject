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

        public CardData(long cardNumber, int expMonth, int expYear,
            string cardName, bool ?isDefault, string addressId, string userId)
        {
            Id = Guid.NewGuid().ToString();
            CardNumber = cardNumber;
            ExpMonth = expMonth;
            ExpYear = expYear;
            CardName = cardName;
            AddressId = addressId;
            UserId = userId;
            if (isDefault == null)
            {
                IsDefault = false;
            }
            else
            {
                IsDefault = (bool)isDefault;
            }

            Bills = new List<Bill>();
        }

    }
}
