using EnergyProject.Common.Models;

namespace EnergyProject.Infrastructure.Interfaces
{
    public interface IMeterRepository : IRepository<Meter>
    {
        public Task<Meter> GetMeterWithMeterReadings(string PaymentAccountId);
    }
}
