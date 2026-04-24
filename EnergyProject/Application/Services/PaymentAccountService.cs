using AspNetCoreGeneratedDocument;
using EnergyProject.Application.Interfaces;
using EnergyProject.Application.Interfaces.Stuff;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Infrastructure.Repositories;
using EnergyProject.Models;
using EnergyProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnergyProject.Application.Services
{
    public class PaymentAccountService : IPaymentAccountService
    {
        private readonly IPaymentAccountRepository _paymentAccountRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ITariffService _tariffService;
        private readonly IAddressRepository _addressRepository;
        private readonly IPowerStatusRepository _powerStatusRepository;
        public PaymentAccountService(IPaymentAccountRepository paymentAccountRepository, ICurrentUserService currentUserService, ITariffService tariffService, IAddressRepository addressRepository, IPowerStatusRepository powerStatusRepository) {
            _paymentAccountRepository = paymentAccountRepository;
            _currentUserService = currentUserService;
            _tariffService = tariffService;
            _addressRepository = addressRepository;
            _powerStatusRepository = powerStatusRepository;
        }
        public List<PaymentAccount> GetAllFullData () 
        { 
            string userId = _currentUserService.GetRequiredUserId();
            return _paymentAccountRepository.GetAllFullData(userId);
        }

        public async Task<PaymentAccountCreateViewModel> Create()
        {
            var vm = new PaymentAccountCreateViewModel();
            
            foreach (var tariff in await _tariffService.GetAll()) {
                vm.TariffOptions.Add(new SelectListItem
                {
                    Value = tariff.Id.ToString(),
                    Text = $" {tariff.Name}, Price per KWh: {tariff.PricePerKWh}"
                });

            }

            return vm;
        }

        public async Task<(bool Succeeded, string ErrorMessage)> CreateAsync(PaymentAccountCreateViewModel model, string userId)
        {
            var activeStatus = await _powerStatusRepository.GetByStatusAsync("Active");
            if (activeStatus == null) return (false, "Active power status not found in DB.");
            var address = await _addressRepository.GetByDetailsAsync(model.City, model.Street, model.House, model.Apartment);

            if (address != null)
            {
                if (address.PaymentAccountId != null)
                {
                    return (false, "A payment account already exists for this address.");
                }
            }
            else
            {
                address = new Address(model.City, model.Street, model.House, model.Apartment, null);
                await _addressRepository.AddAsync(address);
               
            }

            var paymentAccount = new PaymentAccount(
                userId,
                address.Id,
                model.TariffId,
                null,
                activeStatus.Id
            );

            await _paymentAccountRepository.AddAsync(paymentAccount);

            address.PaymentAccountId = paymentAccount.Id;
            await _addressRepository.UpdateAsync(address);

            return (true, string.Empty);
        }

    }
}
