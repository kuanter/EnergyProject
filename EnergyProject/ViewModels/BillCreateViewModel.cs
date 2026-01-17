using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnergyProject.ViewModels
{
    public class BillCreateViewModel
    {
        public string? CardDataId { get; set; }
        public List<SelectListItem> CardDataOptions { get; set; } = new();
    }
}
