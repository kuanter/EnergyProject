using EnergyProject.Data;
using EnergyProject.Models;
using EnergyProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace EnergyProject.Areas.Client.Controllers
{
    [Area("Client")]
    public class BillController : Controller
    {
        ApplicationDbContext db;
        public BillController(ApplicationDbContext db_)
        {
            db = db_;
        }

        public IActionResult Show(string Id)
        {
            var bills = db.Bills
                .Where(b => b.PaymentAccountId == Id)
                .Include(b => b.CardData)
                .ToList();

            float lastBillAmount = 0;    
            float lastReadingAmount = 0; 

            var lastBill = db.Bills
                .Where(b => b.PaymentAccountId == Id)
                .Where(b => b.Status == "Paid")
                .OrderByDescending(b => b.GeneratedAt)
                .FirstOrDefault();

       
            var meter = db.Meters
                .Include(m => m.MeterReadings)
                .Where(m => m.PaymentAccountId == Id)
                .FirstOrDefault();

            if (meter != null)
            {
                var lastMeterReading = db.MeterReadings
                    .Where(mr => mr.MeterId == meter.Id) 
                    .OrderByDescending(m => m.CreatedAt)
                    .FirstOrDefault();

                if (lastMeterReading != null)
                    lastReadingAmount = lastMeterReading.ValueKWh;

                if (lastBill != null)
                {
                    var lastPaidReading = db.MeterReadings
                        .Where(mr => mr.MeterId == meter.Id && mr.CreatedAt <= lastBill.GeneratedAt) 
                        .OrderByDescending(mr => mr.CreatedAt)                                    
                        .FirstOrDefault();                                                        

                    if (lastPaidReading != null)
                        lastBillAmount = lastPaidReading.ValueKWh;
                }
            }

            float totalAmount = lastReadingAmount - lastBillAmount; 
            if (totalAmount < 0) totalAmount = 0;                   
            totalAmount = MathF.Round(totalAmount, 2);            

            var pa = db.PaymentAccounts
                .Where(p => p.Id == Id)
                .FirstOrDefault();

            if (pa == null) return NotFound(); 

            var tariff = db.Tariffs
                .Where(p => p.Id == pa.TariffId)
                .FirstOrDefault();

            if (tariff == null) return NotFound(); 

            float amountToPay = MathF.Round(totalAmount * tariff.PricePerKWh, 2);


            TempData["Amount"] = amountToPay.ToString(CultureInfo.InvariantCulture);   
            TempData["Consumption"] = totalAmount.ToString(CultureInfo.InvariantCulture);
            TempData["PaymentAccount"] = Id.ToString();

            ViewBag.DueAmount = amountToPay;

            return View(bills);
        }

        public IActionResult Create()
        {
  
            TempData.Keep("PaymentAccount"); 
            TempData.Keep("Consumption");   
            TempData.Keep("Amount");        

            var user = db.Users
                .Include(u => u.PaymentAccounts)
                .ThenInclude(m => m.Meter)
                .ThenInclude(mr => mr.MeterReadings)
                .Where(u => u.Id == "U01").First();

            var pa = user.PaymentAccounts.ToList();

            BillCreateViewModel billCreateViewModel = new BillCreateViewModel();

            billCreateViewModel.CardDataOptions = db.CardDatas
                .Where(cd => cd.UserId == "U01")
                .Select(cd => new SelectListItem
                {
                    Value = cd.Id.ToString(),
                    Text = $"Card Number: {cd.CardNumber}, Exp: {cd.ExpMonth}/{cd.ExpYear}"
                }).ToList();

            return View(billCreateViewModel);
        }

        [HttpPost]
        public IActionResult CreatePost(BillCreateViewModel billCreateView)
        {
            Bill bill = new Bill();
            bill.Id = Guid.NewGuid().ToString();
            bill.Status = "Paid";
            bill.GeneratedAt = DateTime.Now;
            bill.PaymentAccountId = TempData.Peek("PaymentAccount")?.ToString(); 
            bill.ConsumptionKWh = float.Parse(TempData.Peek("Consumption")?.ToString() ?? "0", CultureInfo.InvariantCulture);
            bill.Amount = float.Parse(TempData.Peek("Amount")?.ToString() ?? "0", CultureInfo.InvariantCulture);             

            bill.CardDataId = billCreateView.CardDataId;
            db.Bills.Add(bill);
            db.SaveChanges();

            return RedirectToAction("Show", new { Id = bill.PaymentAccountId });
        }

    }
}
