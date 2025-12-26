using EnergyProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EnergyProject.ViewModels
{
    public class CardDataCreateViewModel
    {
        [Required(ErrorMessage = "Card number is required.")]
        [RegularExpression(@"^\d{13,19}$", ErrorMessage = "Card number must contain 13-19 digits.")]
        public long CardNumber { get; set; }

        [Required(ErrorMessage = "Expiration month is required.")]
        [Range(1, 12, ErrorMessage = "Expiration month must be between 1 and 12.")]
        public int ExpMonth { get; set; }


        [Required(ErrorMessage = "Expiration year is required.")]
        [Range(2020, 2100, ErrorMessage = "Expiration year is not valid.")]
        public int ExpYear { get; set; }


        [Required(ErrorMessage = "Name on card is required.")]
        [StringLength(64, ErrorMessage = "Name on card is too long.")]
        public string CardName { get; set; }
        public bool IsDefault { get; set; }
        
        [BindNever]
        public string AddressId { get; set; }

        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Apartment { get; set; }
    }
}
