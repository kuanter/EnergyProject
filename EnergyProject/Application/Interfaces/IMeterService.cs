using EnergyProject.Models;
using EnergyProject.ViewModels;

namespace EnergyProject.Application.Interfaces
{
    public interface IMeterService
    {
        public Task<List<Meter>> Show();
        public Task Create(MeterCreateViewModel meterCreateViewModel);
        public Task SwitchMeterStatus(string Id);

        public Task<Meter> GetMeterWithMeterReadings(string PaymentAccountId);
    }
}
