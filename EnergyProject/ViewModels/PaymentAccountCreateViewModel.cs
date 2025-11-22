using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnergyProject.ViewModels
{
    public class PaymentAccountCreateViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public int TariffId { get; set; }
      
        public List<SelectListItem> AddressOptions { get; set; } = new();
        public List<SelectListItem> TariffOptions { get; set; } = new();

    }
}
