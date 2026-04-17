using EnergyProject.Application.Interfaces;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Models;
using EnergyProject.ViewModels;

namespace EnergyProject.Application.Services
{
    public class MeterService : IMeterService
    {
        private readonly IMeterRepository _meterRepository;

        public MeterService(IMeterRepository meterRepository)
        {
            _meterRepository = meterRepository;
        }

        public async Task<List<Meter>> Show()
        {
            return await _meterRepository.GetAll();
        }

        public async Task Create(MeterCreateViewModel meterCreateViewModel)
        {
            Meter meter = new Meter(meterCreateViewModel.SerialNumber, meterCreateViewModel.PaymentAccountId);
            await _meterRepository.Create(meter);

        }
        public async Task SwitchMeterStatus(string Id)
        {
            var meter = await _meterRepository.GetById(Id);
            if (meter != null)
            {
                meter.IsActive = !meter.IsActive;
                await _meterRepository.Update(meter);
            }

        }
    }
}
