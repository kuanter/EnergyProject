namespace EnergyProject.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Apartment { get; set; }
        public int CardDataId { get; set; }
        public CardData CardData { get; set; }
        public int PaymentAccountId { get; set; }
        public PaymentAccount PaymentAccount { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
