using EnergyProject.Models;

namespace EnergyProject.Infrastructure.Interfaces
{
    public interface IMeterReadingRepository : IRepository<MeterReading>
    {
        public void AddReadingOnInfo(string id, float inc);
    }
}
