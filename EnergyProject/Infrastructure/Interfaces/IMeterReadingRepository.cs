using EnergyProject.Common.Models;

namespace EnergyProject.Infrastructure.Interfaces
{
    public interface IMeterReadingRepository
    {
        public void AddReading(string id, float inc);
        public List<MeterReading> GetMeterReadings(string Id, DateTime start, DateTime end);
    }
}