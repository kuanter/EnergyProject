using EnergyProject.Application.Interfaces;
using EnergyProject.Application.Interfaces.Stuff;
using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Models;
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
        public BillController(ApplicationDbContext db_, ILogger<HomeController> logger, IBillService billService, IConsumptionService consumptionService)
        {
            db = db_;
            _logger = logger;
            _billService = billService;
            _consumptionService = consumptionService;
        }


        public async Task<IActionResult> Show(string Id)
        {
            List<Bill> bills = await _billService.GetBill(Id);
            ConsumptionViewModel consumptionViewModel = await _consumptionService.GetConsumptionViewModel(Id, bills);

            TempData["Amount"] = consumptionViewModel.Amount.ToString(CultureInfo.InvariantCulture);
            TempData["Consumption"] = consumptionViewModel.Consumption.ToString(CultureInfo.InvariantCulture);
            TempData["PaymentAccount"] = Id.ToString();

            _logger.LogInformation("Set TempData");

            ViewBag.DueAmount = consumptionViewModel.Amount;

            return View(bills);
        }

        public IActionResult Create()
        {
            _logger.LogInformation("Used CreateBillController");

            TempData.Keep("PaymentAccount");
            TempData.Keep("Consumption");
            TempData.Keep("Amount");

            _logger.LogInformation("Keep TempData");

            var user = db.Users
                .Include(u => u.PaymentAccounts)
                .ThenInclude(m => m.Meter)
                .ThenInclude(mr => mr.MeterReadings).First();

            _logger.LogInformation("Get user");

            var pa = user.PaymentAccounts.ToList();

            _logger.LogInformation("Get paymentAccounts");

            BillCreateViewModel billCreateViewModel = new BillCreateViewModel();

            billCreateViewModel.CardDataOptions = db.CardDatas
                .Select(cd => new SelectListItem
                {
                    Value = cd.Id.ToString(),
                    Text = $"Card Number: {cd.CardNumber}, Exp: {cd.ExpMonth}/{cd.ExpYear}"
                }).ToList();

            _logger.LogInformation("Get cardDatas");


            return View(billCreateViewModel);
        }

        [HttpPost]
        public IActionResult CreatePost(BillCreateViewModel billCreateView)
        {
            _logger.LogInformation("Used CreatePostBillController");

            Bill bill = new Bill();
            bill.Id = Guid.NewGuid().ToString();
            bill.Status = "Paid";
            bill.GeneratedAt = DateTime.Now;
            bill.PaymentAccountId = TempData.Peek("PaymentAccount")?.ToString();
            bill.ConsumptionKWh = float.Parse(TempData["Consumption"]?.ToString() ?? "0", CultureInfo.InvariantCulture);
            bill.Amount = float.Parse(TempData.Peek("Amount")?.ToString() ?? "0", CultureInfo.InvariantCulture);

            _logger.LogInformation("Set bill data");

            bill.CardDataId = billCreateView.CardDataId;
            db.Bills.Add(bill);
            db.SaveChanges();

            _logger.LogInformation("Save bill");

            return RedirectToAction("Show", new { Id = bill.PaymentAccountId });
        }

    }
}