using EnergyProject.Application.Interfaces;
using EnergyProject.Infrastructure.Data;
using EnergyProject.Common.Models;
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
        private readonly ILogger _logger;
        private IPaymentAccountService _paymentAccountService;
        public PaymentAccountController(ILogger<HomeController> logger, IPaymentAccountService paymentAccountService)
        {
            _logger = logger;
            _paymentAccountService = paymentAccountService;
        }
        public IActionResult Show()
        {
            _logger.LogInformation("Get paymentAccounts");
            return View(_paymentAccountService.GetListByCurrUser());
            
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
            var result = await _paymentAccountService.CreateAsync(model);

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
