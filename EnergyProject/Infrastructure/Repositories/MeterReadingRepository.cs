using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnergyProject.Infrastructure.Repositories
{
    public class MeterReadingRepository : IMeterReadingRepository
    {
        public readonly ApplicationDbContext db;
        public MeterReadingRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public void AddReading(string id, float inc)
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

        public List<MeterReading> GetMeterReadings(string Id)
        {
            var meter = db.Meters
                .Include(m => m.MeterReadings.OrderByDescending(r => r.CreatedAt))
                .FirstOrDefault(m => m.Id == Id);

            return meter.MeterReadings.ToList();
        }
    }
}