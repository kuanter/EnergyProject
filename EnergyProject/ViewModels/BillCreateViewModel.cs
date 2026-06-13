using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EnergyProject.ViewModels
{
    public class BillCreateViewModel
    {
        [Required(ErrorMessage = "Card is required.")]
        public string? CardDataId { get; set; }
        public List<SelectListItem> CardDataOptions { get; set; } = new();

        [Required]
        public string PaymentAccountId { get; set; }
        
        public float AmountToPay { get; set; }
        public float Consumption { get; set; }
    }
}
