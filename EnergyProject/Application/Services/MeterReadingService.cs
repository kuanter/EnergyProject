using EnergyProject.Application.Interfaces;
using EnergyProject.Common.Enums;
using EnergyProject.Common.Models;
using EnergyProject.Infrastructure.Interfaces;

namespace EnergyProject.Application.Services
{
    public class MeterReadingService : IMeterReadingService
    {
        private readonly IMeterReadingRepository _meterReadingRepository;
        public MeterReadingService(IMeterReadingRepository meterReadingRepository)
        {
            _meterReadingRepository = meterReadingRepository;
        }

        public List<MeterReading> GetMeterReadings(string Id, DateTimeFilter dateTimeFilter)
        {
            DateTime start;
            DateTime end = DateTime.Now;
            switch (dateTimeFilter)
            {
                case DateTimeFilter.Day:
                    start = end.AddDays(-1);
                    break;
                case DateTimeFilter.Month:
                    start = end.AddMonths(-1);
                    break;
                case DateTimeFilter.Year:
                    start = end.AddYears(-1);
                    break;
                default:
                    start = end;
                    break;
            }

            return _meterReadingRepository.GetMeterReadings(Id, start, end);
        }

        public void GenerateReading(string Id)
        {
            var lastReading = _meterReadingRepository.GetLastMeterReading(Id);
            float lastValue = lastReading != null ? lastReading.ValueKWh : 0f;
            var inc = (float)(Random.Shared.NextDouble() * (10.00 - 0.10) + 0.10);
            inc = MathF.Round(inc, 2);
            _meterReadingRepository.AddReading(Id, inc, lastValue);
        }

        public MeterReading GetLastMeterReading(string meterId)
        {
            return _meterReadingRepository.GetLastMeterReading(meterId);
        }
    }
}