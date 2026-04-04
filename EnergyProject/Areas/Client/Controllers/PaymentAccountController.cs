using EnergyProject.Infrastructure.Data;
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
        private readonly ILogger _logger;
        public PaymentAccountController(ApplicationDbContext db_, ILogger<HomeController> logger)
        {
            _logger = logger;
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

            _logger.LogInformation("Get paymentAccounts");
            return View(pa);
            
        }
        
        public IActionResult Create() {
            _logger.LogInformation("Used CreatePaymentAccountController");

            var vm = new PaymentAccountCreateViewModel();
            vm.TariffOptions = db.Tariffs.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = $" {t.Name}, Price per KWh: {t.PricePerKWh}"
            }).ToList();

            _logger.LogInformation("Get tariffs");

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PaymentAccountCreateViewModel paymentAccountCreateViewModel)
        {
            _logger.LogInformation("Used CreatePostPaymentAccountController");

            ModelState.Remove(nameof(paymentAccountCreateViewModel.AddressId));
            bool exists = db.Addresses.Any(x =>
                x.City == paymentAccountCreateViewModel.City &&
                x.Street == paymentAccountCreateViewModel.Street &&
                x.House == paymentAccountCreateViewModel.House &&
                x.Apartment == paymentAccountCreateViewModel.Apartment
            );

            _logger.LogInformation("Check address");

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

                _logger.LogInformation("Get address");

                if (address.PaymentAccountId != null) 
                {
                    _logger.LogInformation("paymentAccount exists");
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
                _logger.LogInformation("Add address");
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

            _logger.LogInformation("Set paymentAccount data");

            ModelState.Remove("");
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("ModelState is invalid");
                return RedirectToAction(nameof(Create));
            }
            db.PaymentAccounts.Add(paymentAccount);
            _logger.LogInformation("Add paymentAccount");

            await db.SaveChangesAsync();
            _logger.LogInformation("Save paymentAccount");

            return RedirectToAction("Show", "PaymentAccount");
        }

    }
}
