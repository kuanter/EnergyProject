using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Infrastructure.Data;
using EnergyProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using EnergyProject.Common.Models;

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
    }
}
