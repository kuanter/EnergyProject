namespace EnergyProject.Models
{ 
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }

        public ICollection<CardData> Cards { get; set; }
        public ICollection<PaymentAccount> PaymentAccounts { get; set; }
        public PowerStatus PowerStatus { get; set; }
    }
}
