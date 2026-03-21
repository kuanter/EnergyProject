using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergyProject.Models
{
    public class Client
    {
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        public Client() { }

        public Client(string userId)
        {
            UserId = userId;
        }

    }
}
