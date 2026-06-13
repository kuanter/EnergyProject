using EnergyProject.Common.Models;

namespace EnergyProject.Infrastructure.Interfaces
{
    public interface IMeterReadingRepository
    {
        public void AddReading(string id, float inc, float lastMeterReading);
        public List<MeterReading> GetMeterReadings(string Id, DateTime start, DateTime end);

        public MeterReading GetLastMeterReading(string meterId);
    }
}