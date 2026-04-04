using EnergyProject.Application.Interfaces;
using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyProject.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> Show(string? q)
        {
            if (!string.IsNullOrWhiteSpace(q))
                q = q.Trim();
            var users = _userRepository.GetUsersWithFilters(q);

            return users;
        }
    }
}