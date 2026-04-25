using EnergyProject.Models;

namespace EnergyProject.Infrastructure.Interfaces
{
    public interface IBillRepository
    {
        public Task<List<Bill>> GetBillWithCardData(string PaymentAccountId);

        public Task<Bill> GetLastPaidBill(string PaymentAccountId);
    }
}
