using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyProject.Infrastructure.Repositories
{
    public class PowerStatusRepository : IPowerStatusRepository
    {
        private readonly ApplicationDbContext _db;

        public PowerStatusRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<PowerStatus?> GetByStatusAsync(string statusName)
        {
            return await _db.PowerStatuses.FirstOrDefaultAsync(ps => ps.Status == statusName);
        }
    }
}
