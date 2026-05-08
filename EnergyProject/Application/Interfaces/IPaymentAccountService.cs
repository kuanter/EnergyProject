using EnergyProject.Models;
using EnergyProject.ViewModels;

namespace EnergyProject.Application.Interfaces
{
    public interface IPaymentAccountService
    {
        public List<PaymentAccount> GetListByCurrUser();
        public Task<PaymentAccountCreateViewModel> Create();
        public Task<(bool Succeeded, string ErrorMessage)> CreateAsync(PaymentAccountCreateViewModel model);
    }
}
