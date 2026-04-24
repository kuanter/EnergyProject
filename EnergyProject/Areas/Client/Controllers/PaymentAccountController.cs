using EnergyProject.Application.Interfaces;
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
        private IPaymentAccountService _paymentAccountService;
        public PaymentAccountController(ApplicationDbContext db_, ILogger<HomeController> logger, IPaymentAccountService paymentAccountService)
        {
            _logger = logger;
            db = db_;
            _paymentAccountService = paymentAccountService;
        }
        public IActionResult Show()
        {
            _logger.LogInformation("Get paymentAccounts");
            return View(_paymentAccountService.GetAllFullData());
            
        }

        public async Task<IActionResult> Create() {
            _logger.LogInformation("Used CreatePaymentAccountController");

            _logger.LogInformation("Get tariffs");

            return View(await _paymentAccountService.Create());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PaymentAccountCreateViewModel model)
        {
            _logger.LogInformation("Used CreatePost PaymentAccountController");

            ModelState.Remove(nameof(model.AddressId));

            if (!ModelState.IsValid)
            {
                var freshVm = await _paymentAccountService.Create();
                model.TariffOptions = freshVm.TariffOptions;
                return View("Create", model);
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _paymentAccountService.CreateAsync(model, userId);

            if (!result.Succeeded)
            {
                _logger.LogInformation("PaymentAccount creation failed: " + result.ErrorMessage);
                ModelState.AddModelError(string.Empty, result.ErrorMessage);

                var freshVm = await _paymentAccountService.Create();
                model.TariffOptions = freshVm.TariffOptions;

                return View("Create", model);
            }

            _logger.LogInformation("PaymentAccount created successfully");
            return RedirectToAction("Show", "PaymentAccount");
        }

    }
}
