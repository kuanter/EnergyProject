using EnergyProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                .OrderByDescending(b => b.GeneratedAt).FirstOrDefault();

            if (lastBill != null) 
            { 
                lastBillAmount = lastBill.Amount;
            }

            var meter = db.Meters
                .Include(m => m.MeterReadings)
                .Where(m => m.PaymentAccountId == Id)
                .FirstOrDefault();

            if (meter != null)
            {
                var lastMeterReading = db.MeterReadings
                    .Where(mr => mr.Meter.Id == meter.Id)
                    .OrderByDescending(m => m.CreatedAt).FirstOrDefault();

                if (lastMeterReading != null)
                    lastReadingAmount = lastMeterReading.ValueKWh;

            }    
            
            float totalAmount = lastReadingAmount-lastBillAmount;

            var pa = db.PaymentAccounts
                .Where(p => p.Id == Id)
                .FirstOrDefault();

            var tariff = db.Tariffs
                .Where(p => p.Id == pa.TariffId)
                .FirstOrDefault();

            ViewData["Amount"] = totalAmount * tariff.PricePerKWh;

            return View(bills);
        }
        /*public IActionResult Create() 
        {
            var user = db.Users
                .Include(u => u.PaymentAccounts)
                .ThenInclude(m => m.Meter)
                .ThenInclude(mr => mr.MeterReadings)
                
                .Where(u => u.Id == "U01").First();

            var pa = user.PaymentAccounts.ToList();


        }*/
    }
}
