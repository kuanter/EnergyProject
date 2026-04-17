namespace EnergyProject.Models
{
    public class Address
    {
        public string  Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Apartment { get; set; }
        public ICollection<CardData> CardDatas { get; set; }
        //todo
        public string? PaymentAccountId { get; set; }
        public PaymentAccount? PaymentAccount { get; set; }

        public Address()
        {
            CardDatas = new List<CardData>();
        }

        public Address(string city, string street, string house, string apartment, string? paymentAccountId = null)
        {
            Id = Guid.NewGuid().ToString();
            City = city;
            Street = street;
            House = house;
            Apartment = apartment;
            PaymentAccountId = paymentAccountId;
            CardDatas = new List<CardData>();
        }

    }
}
