using EnergyProject.Data;
using EnergyProject.Models;
using EnergyProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace EnergyProject.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Policy = "ClientOnly")]
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
             .Where(P => P.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
             .Include(P => P.Tariff)
             .Include(P => P.Address)
             .Include(P => P.Meter)
             .Include(P => P.PowerStatus)
             .ToList();
            return View(pa);
            
        }
        
        public IActionResult Create() {
            var vm = new PaymentAccountCreateViewModel();
            vm.TariffOptions = db.Tariffs.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = $" {t.Name}, Price per KWh: {t.PricePerKWh}"
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
            paymentAccount.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
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
                address.PaymentAccountId = paymentAccount.Id;

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
            foreach (var ps in db.PowerStatuses)
            {
                if (ps.Status == "Active")
                {
                    paymentAccount.PowerStatusId = ps.Id;
                    break;
                }
            }
         

            ModelState.Remove("");
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Create));

            db.PaymentAccounts.Add(paymentAccount);
            await db.SaveChangesAsync();
            return RedirectToAction("Show", "PaymentAccount");
        }

    }
}
