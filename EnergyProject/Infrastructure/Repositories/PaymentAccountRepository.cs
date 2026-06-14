using EnergyProject.Common.Models;
using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnergyProject.Infrastructure.Repositories
{
    public class PaymentAccountRepository : Repository<PaymentAccount>, IPaymentAccountRepository
    {
        public PaymentAccountRepository(ApplicationDbContext db) : base(db){}
        public List<PaymentAccount> GetListByUserIdFullData(string userId) 
        {
            var pa = _db.PaymentAccounts
             .Where(P => P.UserId == userId)
             .Include(P => P.Tariff)
             .Include(P => P.Address)
             .Include(P => P.Meter)
             .Include(P => P.PowerStatus)
             .ToList();

            return pa;
        }

        public async Task<PaymentAccount?> GetByIdIgnoreFilter(string id)
        {
            return await _db.PaymentAccounts
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(pa => pa.Id == id);
        }
    }
}
