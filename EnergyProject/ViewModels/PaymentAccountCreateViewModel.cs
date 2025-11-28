using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnergyProject.ViewModels
{
    public class PaymentAccountCreateViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string AddressId { get; set; }
        public string TariffId { get; set; }

        public List<SelectListItem> AddressOptions { get; set; } = new();
        public List<SelectListItem> TariffOptions { get; set; } = new();

    }
}
