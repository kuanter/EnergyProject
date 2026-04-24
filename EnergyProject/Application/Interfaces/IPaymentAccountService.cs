using EnergyProject.Models;
using EnergyProject.ViewModels;

namespace EnergyProject.Application.Interfaces
{
    public interface IPaymentAccountService
    {
        public List<PaymentAccount> GetAllFullData();
        public Task<PaymentAccountCreateViewModel> Create();
        public Task<(bool Succeeded, string ErrorMessage)> CreateAsync(PaymentAccountCreateViewModel model, string userId);
    }
}
