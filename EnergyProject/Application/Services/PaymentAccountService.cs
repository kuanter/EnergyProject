using AspNetCoreGeneratedDocument;
using EnergyProject.Application.Interfaces;
using EnergyProject.Application.Interfaces.Stuff;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Models;

namespace EnergyProject.Application.Services
{
    public class PaymentAccountService : IPaymentAccountService
    {
        private readonly IPaymentAccountRepository _paymentAccountRepository;
        private readonly ICurrentUserService _currentUserService;
        public PaymentAccountService(IPaymentAccountRepository paymentAccountRepository, ICurrentUserService currentUserService) {
            _paymentAccountRepository = paymentAccountRepository;
            _currentUserService = currentUserService;
        }
        public List<PaymentAccount> GetAllFullData () 
        { 
            string userId = _currentUserService.GetRequiredUserId();
            return _paymentAccountRepository.GetAllFullData(userId);
        }
    }
}
