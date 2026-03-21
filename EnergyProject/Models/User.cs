using Microsoft.AspNetCore.Identity;

namespace EnergyProject.Models
{ 
    public class User : IdentityUser
    {
        public ICollection<CardData> Cards { get; set; }
        public ICollection<PaymentAccount> PaymentAccounts { get; set; }

        public User()
        {
            Cards = new List<CardData>();
            PaymentAccounts = new List<PaymentAccount>();
        }

        public User(string email, string userName, bool emailConfirmed = false)
        {
            Email = email;
            UserName = userName;
            EmailConfirmed = emailConfirmed;
            Cards = new List<CardData>();
            PaymentAccounts = new List<PaymentAccount>();
        }

    }
}
