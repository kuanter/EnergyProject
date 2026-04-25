using EnergyProject.Models;

namespace EnergyProject.Application.Interfaces
{
    public interface IBillService
    {
        public Task<List<Bill>> GetBill(string id);
    }
}
