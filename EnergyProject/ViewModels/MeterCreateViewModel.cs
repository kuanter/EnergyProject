using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnergyProject.ViewModels
{
    public class MeterCreateViewModel
    {
        public string SerialNumber { get; set; }
        public string PaymentAccountId { get; set; }

        public List<SelectListItem> PaymentAccounts { get; set; }
    }
}
