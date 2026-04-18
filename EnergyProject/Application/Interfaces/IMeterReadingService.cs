using EnergyProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnergyProject.Application.Interfaces
{
    public interface IMeterReadingService
    {
        public List<MeterReading> GetMeterReadings(string Id);
    }
}
