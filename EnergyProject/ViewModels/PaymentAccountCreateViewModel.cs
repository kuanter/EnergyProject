using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EnergyProject.ViewModels
{
    public class PaymentAccountCreateViewModel
    {
        [Required(ErrorMessage = "Tariff is required.")]
        public string TariffId { get; set; }
        
        [BindNever]
        public string AddressId { get; set; }
        
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
        
        [Required(ErrorMessage = "Street is required.")]
        public string Street { get; set; }
        
        [Required(ErrorMessage = "House is required.")]
        public string House { get; set; }
        
        [Required(ErrorMessage = "Apartment is required.")]
        public string Apartment { get; set; }
        
        public List<SelectListItem> TariffOptions { get; set; } = new();
    }
}
