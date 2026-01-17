using EnergyProject.Data;
using EnergyProject.Models;
using EnergyProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace EnergyProject.Areas.Client.Controllers
{
    [Area("Client")]
    public class PaymentAccountController : Controller
    {
        ApplicationDbContext db;
        public PaymentAccountController(ApplicationDbContext db_)
        {
            db = db_;
        }
        public IActionResult Show()
        {
            var pa = db.PaymentAccounts
                .Include(P => P.Tariff)
                .Include(P => P.Address)
                .Include(P => P.Meter)
                .Include(P => P.PowerStatus) 
                .Where(pa => pa.UserId == "U01")
                .ToList();
            return View(pa);
            
        }
        
        public IActionResult Create() {
            var vm = new PaymentAccountCreateViewModel();
            vm.TariffOptions = db.Tariffs.Select(t => new SelectListItem {
                Value = t.Id.ToString(), Text = $"Tariff ID: {t.Id}, Price per KWh: {t.PricePerKWh}"
            }).ToList();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PaymentAccountCreateViewModel paymentAccountCreateViewModel)
        {
            ModelState.Remove(nameof(paymentAccountCreateViewModel.AddressId));
            bool exists = db.Addresses.Any(x =>
                x.City == paymentAccountCreateViewModel.City &&
                x.Street == paymentAccountCreateViewModel.Street &&
                x.House == paymentAccountCreateViewModel.House &&
                x.Apartment == paymentAccountCreateViewModel.Apartment
            );

            PaymentAccount paymentAccount = new PaymentAccount();
            paymentAccount.UserId = "U01"; // todo
            paymentAccount.Id = Guid.NewGuid().ToString();

            if (exists)
            {
                var address = db.Addresses.FirstOrDefault(x =>
                    x.City == paymentAccountCreateViewModel.City &&
                    x.Street == paymentAccountCreateViewModel.Street &&
                    x.House == paymentAccountCreateViewModel.House &&
                    x.Apartment == paymentAccountCreateViewModel.Apartment
                );
                if (address.PaymentAccountId != null) 
                { 
                    ModelState.AddModelError(string.Empty, "A payment account already exists for this address.");
                }
                paymentAccountCreateViewModel.AddressId = address.Id;
            }
            else
            {
                Address a = new Address();
                a.Apartment = paymentAccountCreateViewModel.Apartment;
                a.City = paymentAccountCreateViewModel.City;
                a.Street = paymentAccountCreateViewModel.Street;
                a.House = paymentAccountCreateViewModel.House;
                a.Id = Guid.NewGuid().ToString();
                a.PaymentAccountId = paymentAccount.Id;
                paymentAccountCreateViewModel.AddressId = a.Id;
                db.Addresses.Add(a);

            }

            paymentAccount.AddressId = paymentAccountCreateViewModel.AddressId;
            paymentAccount.TariffId = paymentAccountCreateViewModel.TariffId;
            paymentAccount.PowerStatusId = "PS01";


            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Create));

            db.PaymentAccounts.Add(paymentAccount);
            await db.SaveChangesAsync();
            return RedirectToAction("Show", "PaymentAccount");
        }

    }
}
