using EnergyProject.Common.Models;

namespace EnergyProject.Infrastructure.Interfaces
{
    public interface IPaymentAccountRepository : IRepository<PaymentAccount>
    {
        public List<PaymentAccount> GetListByUserIdFullData(string userId);
    }
}
