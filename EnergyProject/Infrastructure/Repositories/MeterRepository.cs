using EnergyProject.Common.Models;
using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace EnergyProject.Infrastructure.Repositories
{
    public class MeterRepository : Repository<Meter>, IMeterRepository
    {
        public MeterRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<Meter> GetMeterWithMeterReadings(string PaymentAccountId) {
            
            var paExists = await _db.PaymentAccounts.AnyAsync(p => p.Id == PaymentAccountId);
            if (!paExists) return null;

            return await _db.Meters
                .Include(m => m.MeterReadings)
                .Where(m => m.PaymentAccountId == PaymentAccountId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Meter>> GetActiveMeters()
        {
            return await _db.Meters
                .Where(m => m.IsActive)
                .ToListAsync();
        }
    }
}
