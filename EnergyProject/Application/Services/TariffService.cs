using EnergyProject.Application.Interfaces;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Models;

namespace EnergyProject.Application.Services
{
    public class TariffService : ITariffService
    {
        private IRepository<Tariff> _repository;
        public TariffService(IRepository<Tariff> repository) 
        { 
            _repository = repository;
        }

        public async Task<List<Tariff>> Show() 
        {
            return await _repository.GetAll();
        }

        public async Task<Tariff> GetById(string Id) 
        { 
            return await _repository.GetById(Id);
        }

        public async Task Create(Tariff t) 
        {
            t.Id = Guid.NewGuid().ToString();
            await _repository.Create(t);
        }

        public async Task Update(Tariff t) 
        { 
            await _repository.Update(t);
        }

        public async Task Delete(string id) 
        { 
            await _repository.Delete(await _repository.GetById(id));
        }
    }
}
