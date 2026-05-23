using EnergyProject.Common.Models;
using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnergyProject.Infrastructure.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {

        public AddressRepository(ApplicationDbContext db) : base(db){}

        public async Task<Address?> GetByDetails(string city, string street, string house, string apartment)
        {
            return await _db.Addresses.FirstOrDefaultAsync(x =>
                x.City == city &&
                x.Street == street &&
                x.House == house &&
                x.Apartment == apartment);
        }

      
    }
}
