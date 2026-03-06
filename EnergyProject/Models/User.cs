using Microsoft.AspNetCore.Identity;

namespace EnergyProject.Models
{ 
    public class User : IdentityUser
    {
        public ICollection<CardData> Cards { get; set; }
        public ICollection<PaymentAccount> PaymentAccounts { get; set; }
      
    }
}
