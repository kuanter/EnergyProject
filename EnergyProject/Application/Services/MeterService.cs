using EnergyProject.Application.Interfaces;
using EnergyProject.Common.Models;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.ViewModels;

namespace EnergyProject.Application.Services
{
    public class MeterService : IMeterService
    {
        private readonly IMeterRepository _meterRepository;
        private readonly IPaymentAccountRepository _paymentAccountRepository;
        private readonly IPowerStatusRepository _powerStatusRepository;

        public MeterService(IMeterRepository meterRepository, IPaymentAccountRepository paymentAccountRepository, IPowerStatusRepository powerStatusRepository)
        {
            _meterRepository = meterRepository;
            _paymentAccountRepository = paymentAccountRepository;
            _powerStatusRepository = powerStatusRepository;
        }

        public async Task<List<Meter>> Show()
        {
            return await _meterRepository.GetAll();
        }

        public async Task Create(MeterCreateViewModel meterCreateViewModel)
        {
            Meter meter = new Meter(meterCreateViewModel.SerialNumber, meterCreateViewModel.PaymentAccountId);
            await _meterRepository.Create(meter);

            await UpdatePaymentAccountPowerStatus(meter.PaymentAccountId, meter.IsActive);
        }

        public async Task SwitchMeterStatus(string Id)
        {
            var meter = await _meterRepository.GetById(Id);
            if (meter != null)
            {
                meter.IsActive = !meter.IsActive;
                await _meterRepository.Update(meter);

                await UpdatePaymentAccountPowerStatus(meter.PaymentAccountId, meter.IsActive);
            }
        }

        public async Task<Meter> GetMeterWithMeterReadings(string PaymentAccountId)
        {
            return await _meterRepository.GetMeterWithMeterReadings(PaymentAccountId);
        }

        public async Task<List<Meter>> GetActiveMeters()
        {
            return await _meterRepository.GetActiveMeters();
        }

        private async Task UpdatePaymentAccountPowerStatus(string paymentAccountId, bool isMeterActive)
        {
            var paymentAccount = await _paymentAccountRepository.GetByIdIgnoreFilter(paymentAccountId);
            if (paymentAccount == null) return;

            var statusName = isMeterActive ? "Active" : "Inactive";
            var powerStatus = await _powerStatusRepository.GetByStatus(statusName);
            if (powerStatus == null) return;

            paymentAccount.PowerStatusId = powerStatus.Id;
            await _paymentAccountRepository.Update(paymentAccount);
        }
    }
}