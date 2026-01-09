using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnergyProject.ViewModels
{
    public class PaymentAccountCreateViewModel
    {
        public string TariffId { get; set; }
        
        [BindNever]
        public string AddressId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Apartment { get; set; }
        public List<SelectListItem> TariffOptions { get; set; } = new();

    }
}
