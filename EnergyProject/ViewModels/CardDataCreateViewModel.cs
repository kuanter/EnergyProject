using EnergyProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnergyProject.ViewModels
{
    public class CardDataCreateViewModel
    {
        public long CardNumber { get; set; }
        public string ExpMonth { get; set; }
        public string ExpYear { get; set; }
        public string CardName { get; set; }
        public bool IsDefault { get; set; }
        public string AddressId { get; set; }

        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Apartment { get; set; }
        public List<SelectListItem> Addresses { get; set; }
    }
}
