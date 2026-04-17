using EnergyProject.Infrastructure.Data;
using EnergyProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnergyProject.Infrastructure.Repositories
{
    public class MeterReadingRepository
    {
        public readonly ApplicationDbContext db;
        public MeterReadingRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public void AddReadingOnInfo(string id, float inc)
        {
            var meter = db.Meters.FirstOrDefault(m => m.Id == id);
            if (meter.IsActive == true)
            {
                var last = db.MeterReadings
               .Where(r => r.MeterId == id)
               .OrderByDescending(r => r.CreatedAt)
               .Select(r => r.ValueKWh)
               .FirstOrDefault();
                
                var next = MathF.Round(last + inc, 2);

                db.MeterReadings.Add(new MeterReading(next, id));
                db.SaveChanges();
            }
        }

        public IActionResult Info(string Id)
        {
            AddReadingOnInfo(Id);

            var meterReadings = db.Meters
                .Include(m => m.MeterReadings.OrderByDescending(r => r.CreatedAt))
                .FirstOrDefault(m => m.Id == Id);

            return meterReadings;
        }
    }
}
