using EnergyProject.Models;

namespace EnergyProject.Infrastructure.Interfaces
{
    public interface IPowerStatusRepository
    {
        public Task<PowerStatus?> GetByStatusAsync(string statusName);
    }
}
