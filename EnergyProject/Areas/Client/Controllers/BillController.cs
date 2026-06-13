using EnergyProject.Application.Interfaces;
using EnergyProject.Application.Interfaces.Stuff;
using EnergyProject.Common.Models;
using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EnergyProject.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Policy = "ClientOnly")]
    public class BillController : Controller
    {
        ApplicationDbContext db;
        private readonly ILogger _logger;
        private readonly IBillService _billService;
        private readonly IConsumptionService _consumptionService;
        private readonly ICurrentUserService _currentUserService;
        public BillController(ApplicationDbContext db_, ILogger<HomeController> logger, IBillService billService, IConsumptionService consumptionService, ICurrentUserService currentUserService)
        {
            db = db_;
            _logger = logger;
            _billService = billService;
            _consumptionService = consumptionService;
            _currentUserService = currentUserService;
        }


        public async Task<IActionResult> Show(string Id)
        {
            // Security check: ensure the current user owns this PaymentAccount
            // ApplicationDbContext automatically applies UserId filter to PaymentAccounts
            var paExists = await db.PaymentAccounts.AnyAsync(p => p.Id == Id);
            if (!paExists)
            {
                return RedirectToAction("Show", "PaymentAccount");
            }

            List<Bill> bills = await _billService.GetListByPaymentAccountId(Id);
            ConsumptionViewModel consumptionViewModel = await _consumptionService.GetConsumptionViewModel(Id, bills);

            TempData["Amount"] = consumptionViewModel.Amount.ToString(CultureInfo.InvariantCulture);
            TempData["Consumption"] = consumptionViewModel.Consumption.ToString(CultureInfo.InvariantCulture);
            TempData["PaymentAccount"] = Id.ToString();

            _logger.LogInformation("Set TempData");

            ViewBag.DueAmount = consumptionViewModel.Amount;
            ViewBag.PaymentAccountId = Id;

            return View(bills);
        }

        public async Task<IActionResult> Create(string paymentAccountId)
        {
            _logger.LogInformation("Used CreateBillController");

            var paExists = await db.PaymentAccounts.AnyAsync(p => p.Id == paymentAccountId);
            if (!paExists) return RedirectToAction("Show", "PaymentAccount");

            List<Bill> bills = await _billService.GetListByPaymentAccountId(paymentAccountId);
            ConsumptionViewModel consumptionViewModel = await _consumptionService.GetConsumptionViewModel(paymentAccountId, bills);

            BillCreateViewModel billCreateViewModel = new BillCreateViewModel
            {
                PaymentAccountId = paymentAccountId,
                AmountToPay = consumptionViewModel.Amount,
                Consumption = consumptionViewModel.Consumption
            };

            string currentUserId = _currentUserService.GetRequiredUserId();
            billCreateViewModel.CardDataOptions = db.CardDatas
                .Where(cd => cd.UserId == currentUserId && cd.IsActive)
                .Select(cd => new SelectListItem
                {
                    Value = cd.Id.ToString(),
                    Text = $"Card Number: {cd.CardNumber}, Exp: {cd.ExpMonth}/{cd.ExpYear}"
                }).ToList();

            _logger.LogInformation("Get cardDatas");

            return View(billCreateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(BillCreateViewModel billCreateView)
        {
            _logger.LogInformation("Used CreatePostBillController");

            var currentUserId = _currentUserService.GetRequiredUserId();
            var paExists = await db.PaymentAccounts.AnyAsync(p => p.Id == billCreateView.PaymentAccountId);
            if (!paExists) return RedirectToAction("Show", "PaymentAccount");

            if (!ModelState.IsValid)
            {
                billCreateView.CardDataOptions = db.CardDatas
                    .Where(cd => cd.UserId == currentUserId && cd.IsActive)
                    .Select(cd => new SelectListItem
                    {
                        Value = cd.Id.ToString(),
                        Text = $"Card Number: {cd.CardNumber}, Exp: {cd.ExpMonth}/{cd.ExpYear}"
                    }).ToList();
                return View("Create", billCreateView);
            }

            var cardExists = await db.CardDatas.AnyAsync(c => c.Id == billCreateView.CardDataId && c.UserId == currentUserId && c.IsActive);
            if (!cardExists) 
            {
                ModelState.AddModelError(string.Empty, "Invalid card selected.");
                billCreateView.CardDataOptions = db.CardDatas
                    .Where(cd => cd.UserId == currentUserId && cd.IsActive)
                    .Select(cd => new SelectListItem
                    {
                        Value = cd.Id.ToString(),
                        Text = $"Card Number: {cd.CardNumber}, Exp: {cd.ExpMonth}/{cd.ExpYear}"
                    }).ToList();
                return View("Create", billCreateView);
            }

            List<Bill> bills = await _billService.GetListByPaymentAccountId(billCreateView.PaymentAccountId);
            ConsumptionViewModel consumptionViewModel = await _consumptionService.GetConsumptionViewModel(billCreateView.PaymentAccountId, bills);

            if (consumptionViewModel.Amount <= 0)
            {
                return RedirectToAction("Show", new { Id = billCreateView.PaymentAccountId });
            }

            Bill bill = new Bill();
            bill.Id = Guid.NewGuid().ToString();
            bill.Status = "Paid";
            bill.GeneratedAt = DateTime.Now;
            bill.PaymentAccountId = billCreateView.PaymentAccountId;
            bill.ConsumptionKWh = consumptionViewModel.Consumption;
            bill.Amount = consumptionViewModel.Amount;

            _logger.LogInformation("Set bill data");

            bill.CardDataId = billCreateView.CardDataId;
            db.Bills.Add(bill);
            await db.SaveChangesAsync();

            _logger.LogInformation("Save bill");

            return RedirectToAction("Show", new { Id = bill.PaymentAccountId });
        }

    }
}