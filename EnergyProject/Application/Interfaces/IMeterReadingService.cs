using EnergyProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnergyProject.Application.Interfaces
{
    public interface IMeterReadingService
    {
        public void AddReadingOnInfo(string id);

        public MeterReading Info(string Id);
    }
}
