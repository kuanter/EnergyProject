using EnergyProject.Models;

namespace EnergyProject.Application.Interfaces
{
    public interface IPowerStatusService
    {
        public Task<List<PowerStatus>> Show();
        public Task<PowerStatus> GetById(string Id);
        public Task Create(PowerStatus t);
        public Task Update(PowerStatus t);
        public Task Delete(string id);
    }
}
