using EnergyProject.Common.Models;

namespace EnergyProject.Infrastructure.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        public Task<Address?> GetByDetails(string city, string street, string house, string apartment);
       
    }
}
