using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergyProject.Models
{
    public class Admin
    {
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
