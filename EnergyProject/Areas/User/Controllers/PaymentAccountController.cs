using EnergyProject.Data;
using EnergyProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EnergyProject.Areas.User.Controllers
{
    [Area("User")]
    public class PaymentAccountController : Controller
    {
        ApplicationDbContext db;
        public PaymentAccountController(ApplicationDbContext db_)
        {
            db = db_;
        }
        public IActionResult Show()
        {
            // to do
            var pa = db.PaymentAccounts.ToList();
            return View(pa);
            
        }
        public IActionResult Create() {
            PaymentAccountCreateViewModel paymentAccountCreateViewModel = new PaymentAccountCreateViewModel();
            var cities = db.Addresses.Select(a => a.City).ToList();
            var streets = db.Addresses.Select(a => a.Street).ToList();
            var houses = db.Addresses.Select(a => a.House).ToList();
            var apartments = db.Addresses.Select(a => a.Apartment).ToList();

            List<string> resultAddresses = new List<string>();
            for (int i = 0; i < cities.Count; i++)
            {
                string fullAddress = $"{cities[i]}, {streets[i]}, {houses[i]}, {apartments[i]}";
                resultAddresses.Add(fullAddress);
            }

            paymentAccountCreateViewModel.AddressOptions = resultAddresses;
            return View();
        }
        //public async Task<IActionResult> CreatePost(Models.PaymentAccount pa) { 
        
        //}
    }
}
