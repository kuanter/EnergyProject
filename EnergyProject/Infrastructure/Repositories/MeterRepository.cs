using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Models;

namespace EnergyProject.Infrastructure.Repositories
{
    public class MeterRepository : Repository<Meter>, IMeterRepository
    {
        private readonly ApplicationDbContext _db;

        public MeterRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
