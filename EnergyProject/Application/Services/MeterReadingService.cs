using EnergyProject.Application.Interfaces;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Infrastructure.Repositories;
using EnergyProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EnergyProject.Application.Services
{
    public class MeterReadingService : IMeterReadingService
    {
        private readonly IMeterReadingRepository _meterReadingRepository;
        public MeterReadingService(IMeterReadingRepository meterReadingRepository) {
            _meterReadingRepository = meterReadingRepository;
        }
        public void AddReadingOnInfo(string id)
        {
            var inc = (float)(Random.Shared.NextDouble() * (10.00 - 0.10) + 0.10);
            inc = MathF.Round(inc, 2);

            _meterReadingRepository.AddReadingOnInfo(id, inc);
        }

        public MeterReading Info(string Id) // GetMeterReadings RENAME
        {
            AddReadingOnInfo(Id);

            

            var meterReadings = db.Meters
                .Include(m => m.MeterReadings.OrderByDescending(r => r.CreatedAt))
                .FirstOrDefault(m => m.Id == Id);

            return meterReadings;
        }
    }
}
