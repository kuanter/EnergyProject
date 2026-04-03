using EnergyProject.Application.Interfaces;
using EnergyProject.Data;
using EnergyProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyProject.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<User>> Show(string? q)
        {
            var users = _db.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.Trim();

                users = users.Where(u =>
                    EF.Functions.Like(u.UserName, "%" + q + "%") ||
                    EF.Functions.Like(u.Email, "%" + q + "%") ||
                    EF.Functions.Like(u.PhoneNumber, "%" + q + "%")
                );
            }

            return users.ToList();

        }
    }
}
