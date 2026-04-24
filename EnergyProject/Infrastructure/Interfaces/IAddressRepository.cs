using EnergyProject.Models;

namespace EnergyProject.Infrastructure.Interfaces
{
    public interface IAddressRepository
    {
        public Task<Address?> GetByDetailsAsync(string city, string street, string house, string apartment);
        public Task AddAsync(Address address);
        public Task UpdateAsync(Address address);
    }
}
