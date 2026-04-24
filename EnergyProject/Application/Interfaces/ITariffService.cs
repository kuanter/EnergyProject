using EnergyProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnergyProject.Application.Interfaces
{
    public interface ITariffService
    {
        public Task<List<Tariff>> GetAll();
        public Task<Tariff> GetById(string Id);
        public Task Create(Tariff t);
        public Task Update(Tariff t);
        public Task Delete(string id);
    }
}
