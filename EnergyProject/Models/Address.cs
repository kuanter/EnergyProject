namespace EnergyProject.Models
{
    public class Address
    {
        public string  Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Apartment { get; set; }
        public string CardDataId { get; set; }
        public ICollection<CardData> CardDatas { get; set; }
        //todo
        public string PaymentAccountId { get; set; }
        public PaymentAccount PaymentAccount { get; set; }
       
    }
}
