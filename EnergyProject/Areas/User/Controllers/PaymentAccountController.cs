using EnergyProject.Data;
using EnergyProject.Models;
using EnergyProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


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
            var pa = db.PaymentAccounts
                .Include(P => P.Tariff)
                .Include(P => P.Address)
                .Include(P => P.Meter)
                .Include(P => P.PowerStatus)    
                .ToList();
            return View(pa);
            
        }
        
        public IActionResult Create() {
            var vm = new PaymentAccountCreateViewModel();

            vm.AddressOptions = db.Addresses.Select(a => new SelectListItem{
                Value = a.Id.ToString(),Text = $"{a.City}, {a.Street}, {a.House}, {a.Apartment}"
            }).ToList();

            vm.TariffOptions = db.Tariffs.Select(t => new SelectListItem {
                Value = t.Id.ToString(), Text = $"Tariff ID: {t.Id}, Price per KWh: {t.PricePerKWh}"
            }).ToList();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PaymentAccountCreateViewModel paymentAccountCreateViewModel)
        {
            // db.Users.Update(CurrUser);
            PaymentAccount paymentAccount = new PaymentAccount();
            paymentAccount.UserId = "1";
            paymentAccount.Id = Guid.NewGuid().ToString();
            paymentAccount.AddressId = paymentAccountCreateViewModel.AddressId;
            paymentAccount.TariffId = paymentAccountCreateViewModel.TariffId;
            paymentAccount.MeterId = "1";
            paymentAccount.PowerStatusId = "1";
            db.PaymentAccounts.Add(paymentAccount);
            await db.SaveChangesAsync();
            return RedirectToAction("Show", "PaymentAccount");
        }

    }
}
