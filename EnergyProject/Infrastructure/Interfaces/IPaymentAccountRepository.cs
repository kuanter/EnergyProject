using EnergyProject.Models;

namespace EnergyProject.Infrastructure.Interfaces
{
    public interface IPaymentAccountRepository
    {
        public List<PaymentAccount> GetAllFullData(string userId);
    }
}
