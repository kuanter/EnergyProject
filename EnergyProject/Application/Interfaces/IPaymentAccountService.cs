using EnergyProject.Models;

namespace EnergyProject.Application.Interfaces
{
    public interface IPaymentAccountService
    {
        public List<PaymentAccount> GetAllFullData();
    }
}
