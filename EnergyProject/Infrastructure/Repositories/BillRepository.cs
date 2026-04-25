using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EnergyProject.Infrastructure.Repositories
{
    public class BillRepository : IBillRepository
    {
        private readonly ApplicationDbContext db;
        public BillRepository(ApplicationDbContext db_)
        {
            db = db_;
        }
        public async Task<List<Bill>> GetBillWithCardData(string PaymentAccountId) {
            var bills = db.Bills
                .Where(b => b.PaymentAccountId == PaymentAccountId)
                .Include(b => b.CardData)
                .ToList();
            return bills;
        }
        public async Task<Bill> GetLastPaidBill(string PaymentAccountId) 
        {
            return db.Bills
                .Where(b => b.PaymentAccountId == PaymentAccountId)
                .Where(b => b.Status == "Paid")
                .OrderByDescending(b => b.GeneratedAt)
                .FirstOrDefault();
        }
    }
}
