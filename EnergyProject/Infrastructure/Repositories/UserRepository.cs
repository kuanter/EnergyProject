using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyProject.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<User> GetUsersWithFilters(string? filter)
        {
            return _db.Users.Where(u =>
                    EF.Functions.Like(u.UserName, "%" + filter + "%") ||
                    EF.Functions.Like(u.Email, "%" + filter + "%") || 
                    EF.Functions.Like(u.PhoneNumber, "%" + filter + "%")
                ).ToList();
        }
    }
}