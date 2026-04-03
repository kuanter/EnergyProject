using EnergyProject.Models;

namespace EnergyProject.Application.Interfaces
{
    public interface IUserService
    {
        public Task<List<User>> Show(string? q);
    }
}
