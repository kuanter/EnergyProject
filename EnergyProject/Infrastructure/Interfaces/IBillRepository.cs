using EnergyProject.Common.Models;

namespace EnergyProject.Infrastructure.Interfaces
{
    public interface IBillRepository
    {
        public Task<List<Bill>> GetListWithCardDatasByPaymentAccountId(string PaymentAccountId);

        public Task<Bill> GetLastPaidBill(string PaymentAccountId);
    }
}
