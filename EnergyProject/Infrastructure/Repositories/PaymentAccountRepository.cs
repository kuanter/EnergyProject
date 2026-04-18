using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Models;
using EnergyProject.Infrastructure.Data;
using EnergyProject.Models;
using EnergyProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EnergyProject.Infrastructure.Repositories
{
    public class PaymentAccountRepository : IPaymentAccountRepository
    {
        public readonly ApplicationDbContext db;
        public PaymentAccountRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public List<PaymentAccount> GetAllFullData(string userId) 
        {
            var pa = db.PaymentAccounts
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
