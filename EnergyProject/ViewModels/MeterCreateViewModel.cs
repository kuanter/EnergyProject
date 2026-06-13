using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EnergyProject.ViewModels
{
    public class MeterCreateViewModel
    {
        [Required(ErrorMessage = "Serial Number is required.")]
        public string SerialNumber { get; set; }
        
        [Required(ErrorMessage = "Payment Account is required.")]
        public string PaymentAccountId { get; set; }

        public List<SelectListItem> PaymentAccountOptions { get; set; }
    }
}
