using EnergyProject.Data;
using EnergyProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnergyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MeterReadingController : Controller
    {
        public ApplicationDbContext db;
        public MeterReadingController(ApplicationDbContext context)
        {
            db = context;
        }
        
        private void AddReadingOnInfo(string id)
        {
            var meter = db.Meters.FirstOrDefault(m => m.Id == id);
            if (meter.IsActive == true)
            {
                var last = db.MeterReadings
               .Where(r => r.MeterId == id)
               .OrderByDescending(r => r.CreatedAt)
               .Select(r => r.ValueKWh)
               .FirstOrDefault();
                var inc = (float)(Random.Shared.NextDouble() * (10.00 - 0.10) + 0.10);
                inc = MathF.Round(inc, 2);

                var next = MathF.Round(last + inc, 2);

                db.MeterReadings.Add(new MeterReading
                {
                    Id = Guid.NewGuid().ToString(),
                    MeterId = id,
                    CreatedAt = DateTime.Now,
                    ValueKWh = next
                });

                db.SaveChanges();
            }
        }

        public IActionResult Info(string Id)
        {
           
            AddReadingOnInfo(Id);

            var meterReadings = db.Meters
                .Include(m => m.MeterReadings.OrderByDescending(r => r.CreatedAt))
                .FirstOrDefault(m => m.Id == Id);

            return View(meterReadings);
        }
    }
}
