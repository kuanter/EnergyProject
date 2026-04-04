using EnergyProject.Infrastructure.Data;
using EnergyProject.Models;
using EnergyProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace EnergyProject.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Policy = "ClientOnly")]
    public class BillController : Controller
    {
        ApplicationDbContext db;
        private readonly ILogger _logger;
        public BillController(ApplicationDbContext db_, ILogger<HomeController> logger)
        {
            db = db_;
            _logger = logger;
        }
     
        public IActionResult Show(string Id)
        {
            _logger.LogInformation("Used ShowBillController");

            var bills = db.Bills
                .Where(b => b.PaymentAccountId == Id)
                .Include(b => b.CardData)
                .ToList();

            float lastBillAmount = 0;    
            float lastReadingAmount = 0;

            _logger.LogInformation("Get bills");

            var lastBill = db.Bills
                .Where(b => b.PaymentAccountId == Id)
                .Where(b => b.Status == "Paid")
                .OrderByDescending(b => b.GeneratedAt)
                .FirstOrDefault();

            _logger.LogInformation("Get lastBill");

            var meter = db.Meters
                .Include(m => m.MeterReadings)
                .Where(m => m.PaymentAccountId == Id)
                .FirstOrDefault();

            _logger.LogInformation("Get meter");

            if (meter != null)
            {
                var lastMeterReading = db.MeterReadings
                    .Where(mr => mr.MeterId == meter.Id) 
                    .OrderByDescending(m => m.CreatedAt)
                    .FirstOrDefault();

                _logger.LogInformation("Get lastMeterReading");

                if (lastMeterReading != null)
                {
                    lastReadingAmount = lastMeterReading.ValueKWh;
                }
                else
                {
                    _logger.LogInformation("lastMeterReading is null");
                }

                if (lastBill != null)
                {

                    var lastPaidReading = db.MeterReadings
                        .Where(mr => mr.MeterId == meter.Id && mr.CreatedAt <= lastBill.GeneratedAt)
                        .OrderByDescending(mr => mr.CreatedAt)
                        .FirstOrDefault();

                    _logger.LogInformation("Get lastPaidReading");

                    if (lastPaidReading != null)
                        lastBillAmount = lastPaidReading.ValueKWh;
                }
                else
                {
                    _logger.LogInformation("lastBill is null");
                }
            }
            else
            {
                _logger.LogInformation("meter is null");
            }

            float totalAmount = lastReadingAmount - lastBillAmount; 
            if (totalAmount < 0) totalAmount = 0;                   
            totalAmount = MathF.Round(totalAmount, 2);            

            var pa = db.PaymentAccounts
                .Where(p => p.Id == Id)
                .FirstOrDefault();

            _logger.LogInformation("Get paymentAccount");

            if (pa == null)
            {
                _logger.LogInformation("paymentAccount is null");
                return NotFound();
            }

            var tariff = db.Tariffs
                .Where(p => p.Id == pa.TariffId)
                .FirstOrDefault();

            _logger.LogInformation("Get tariff");

            if (tariff == null)
            {
                _logger.LogInformation("tariff is null");
                return NotFound();
            } 

            float amountToPay = MathF.Round(totalAmount * tariff.PricePerKWh, 2);


            TempData["Amount"] = amountToPay.ToString(CultureInfo.InvariantCulture);   
            TempData["Consumption"] = totalAmount.ToString(CultureInfo.InvariantCulture);
            TempData["PaymentAccount"] = Id.ToString();

            _logger.LogInformation("Set TempData");

            ViewBag.DueAmount = amountToPay;

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
