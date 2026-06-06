using EnergyProject.Common.Enums;
using EnergyProject.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnergyProject.Application.Interfaces
{
    public interface IMeterReadingService
    {
        public List<MeterReading> GetMeterReadings(string Id, DateTimeFilter dateTimeFilter);
        public MeterReading GetLastMeterReading(string meterId);

        public void GenerateReading(string Id);
    }
}