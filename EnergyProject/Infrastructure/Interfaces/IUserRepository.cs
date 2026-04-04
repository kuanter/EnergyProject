using EnergyProject.Models;

namespace EnergyProject.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        public List<User> GetUsersWithFilters(string? filter);
    }
}