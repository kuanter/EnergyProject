using EnergyProject.Common.Models;
using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
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
        public void AddReading(string id, float inc, float lastMeterReading)
        {
            var next = MathF.Round(lastMeterReading + inc, 2);

            db.MeterReadings.Add(new MeterReading(next, id));
            db.SaveChanges();
            
        }

        public List<MeterReading> GetMeterReadings(string Id, DateTime start, DateTime end)
        {
            var query = db.MeterReadings
                .Where(r => r.MeterId == Id);

            if (!start.Equals(end))
            {
                query = query.Where(r => r.CreatedAt >= start && r.CreatedAt <= end);
            }

            return query
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
        }

        public MeterReading GetLastMeterReading(string meterId)
        {
            return db.MeterReadings
            .Where(r => r.MeterId == meterId)
            .OrderByDescending(r => r.CreatedAt)
            .FirstOrDefault();
        }

    }
}