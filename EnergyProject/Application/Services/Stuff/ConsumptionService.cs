using EnergyProject.Application.Interfaces;
using EnergyProject.Application.Interfaces.Stuff;
using EnergyProject.Areas.Client.Controllers;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Models;
using EnergyProject.ViewModels;

namespace EnergyProject.Application.Services.Stuff
{
    public class ConsumptionService : IConsumptionService
    {
        private readonly IBillRepository _billRepository;
        private readonly IRepository<PaymentAccount> _paymentAccountRepository;
        private readonly IRepository<Tariff> _tariffRepository;
        private readonly IMeterService _meterService;
        private readonly ILogger _logger;
        public ConsumptionService(IBillRepository billRepository, IMeterService meterService,
            ILogger<HomeController> logger,
            IRepository<PaymentAccount> paymentAccountRepository,
            IRepository<Tariff> tariffRepository
            )
        {
            _billRepository = billRepository;
            _meterService = meterService;
            _logger = logger;
            _paymentAccountRepository = paymentAccountRepository;
            _tariffRepository = tariffRepository;
        }
        public async Task<ConsumptionViewModel> GetConsumptionViewModel(string paymentAccountId, List<Bill> bills)
        {
            ConsumptionViewModel consumptionViewModel = new ConsumptionViewModel();
            var lastPaidBill = await _billRepository.GetLastPaidBill(paymentAccountId);
            _logger.LogInformation("Get lastBill");

            var meterWithMeterReadings = await _meterService.GetMeterWithMeterReadings(paymentAccountId);
            _logger.LogInformation("Get meter");
            if (meterWithMeterReadings != null)
            {
                var lastMeterReading = meterWithMeterReadings.MeterReadings
                    .OrderByDescending(mr => mr.CreatedAt)
                    .FirstOrDefault();

                _logger.LogInformation("Get lastMeterReading");

                float lastBillAmount = 0;
                float lastReadingAmount = 0;

                if (lastMeterReading != null)
                {
                    lastReadingAmount = lastMeterReading.ValueKWh;
                }
                else
                {
                    _logger.LogInformation("lastMeterReading is null");
                }

                if (lastPaidBill != null)
                {
                    var lastPaidReading = meterWithMeterReadings.MeterReadings
                        .Where(mr => mr.CreatedAt <= lastPaidBill.GeneratedAt)
                        .OrderByDescending(mr => mr.CreatedAt)
                        .FirstOrDefault();

                    _logger.LogInformation("Get lastPaidReading");

                    if (lastPaidReading != null)
                        lastBillAmount = lastPaidReading.ValueKWh;
                }
                else
                {
                    _logger.LogInformation("lastBill is null");
                }

                float totalAmount = lastReadingAmount - lastBillAmount;
                if (totalAmount < 0) totalAmount = 0;
                totalAmount = MathF.Round(totalAmount, 2);

                var paymentAccount = await _paymentAccountRepository.GetById(paymentAccountId);

                _logger.LogInformation("Get paymentAccount");

                if (paymentAccount == null)
                {
                    _logger.LogInformation("paymentAccount is null");
                }

                var tariff = await _tariffRepository.GetById(paymentAccount.TariffId);

                _logger.LogInformation("Get tariff");

                if (tariff == null)
                {
                    _logger.LogInformation("tariff is null");
                }

                float amountToPay = MathF.Round(totalAmount * tariff.PricePerKWh, 2);


                consumptionViewModel.Amount = amountToPay;
                consumptionViewModel.Consumption = totalAmount;
            }
            else
            {
                consumptionViewModel.Amount = consumptionViewModel.Consumption = 0;
            }

            return consumptionViewModel;
        }
    }
}
