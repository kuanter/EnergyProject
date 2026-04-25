using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyProject.Infrastructure.Repositories
{
    public class MeterRepository : Repository<Meter>, IMeterRepository
    {
        private readonly ApplicationDbContext _db;

        public MeterRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Meter> GetMeterWithMeterReadings(string PaymentAccountId) {
            return _db.Meters
                .Include(m => m.MeterReadings)
                .Where(m => m.PaymentAccountId == PaymentAccountId)
                .FirstOrDefault();
        }
    }
}
