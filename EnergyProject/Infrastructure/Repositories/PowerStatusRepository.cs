using EnergyProject.Common.Models;
using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnergyProject.Infrastructure.Repositories
{
    public class PowerStatusRepository : Repository<PowerStatus>, IPowerStatusRepository
    {
        public PowerStatusRepository(ApplicationDbContext db) : base(db) {}

        public async Task<PowerStatus> GetByStatus(string statusName)
        {
            return await _db.PowerStatuses.FirstOrDefaultAsync(ps => ps.Status == statusName);
        }
    }
}
