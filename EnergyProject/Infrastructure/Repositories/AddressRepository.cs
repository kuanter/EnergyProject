using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyProject.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _db;

        public AddressRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Address?> GetByDetailsAsync(string city, string street, string house, string apartment)
        {
            return await _db.Addresses.FirstOrDefaultAsync(x =>
                x.City == city &&
                x.Street == street &&
                x.House == house &&
                x.Apartment == apartment);
        }

        public async Task AddAsync(Address address)
        {
            await _db.Addresses.AddAsync(address);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Address address)
        {
            _db.Addresses.Update(address);
            await _db.SaveChangesAsync();
        }
    }
}
