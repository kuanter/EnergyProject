using EnergyProject.Common.Models;

namespace EnergyProject.Application.Interfaces
{
    public interface IBillService
    {
        public Task<List<Bill>> GetListByPaymentAccountId(string id);
    }
}
