using EnergyProject.Application.Interfaces;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Models;

public class MeterReadingService : IMeterReadingService
{
    private readonly IMeterReadingRepository _meterReadingRepository;
    public MeterReadingService(IMeterReadingRepository meterReadingRepository)
    {
        _meterReadingRepository = meterReadingRepository;
    }

    public List<MeterReading> GetMeterReadings(string Id)
    {
        var inc = (float)(Random.Shared.NextDouble() * (10.00 - 0.10) + 0.10);
        inc = MathF.Round(inc, 2);

        _meterReadingRepository.AddReading(Id, inc);

        return _meterReadingRepository.GetMeterReadings(Id);
    }
}