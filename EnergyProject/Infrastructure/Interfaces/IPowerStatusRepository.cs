using EnergyProject.Common.Models;

namespace EnergyProject.Infrastructure.Interfaces
{
    public interface IPowerStatusRepository : IRepository<PowerStatus>
    {
        public Task<PowerStatus> GetByStatus(string statusName);
    }
}
