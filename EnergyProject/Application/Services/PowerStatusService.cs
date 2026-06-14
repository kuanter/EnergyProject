using EnergyProject.Application.Interfaces;
using EnergyProject.Common.Models;
using EnergyProject.Infrastructure.Interfaces;

namespace EnergyProject.Application.Services
{
    public class PowerStatusService : IPowerStatusService
    {
        private IPowerStatusRepository _powerStatusRepository;
        public PowerStatusService(IPowerStatusRepository powerStatusRepository)
        {
            _powerStatusRepository = powerStatusRepository;
        }
        public async Task<List<PowerStatus>> Show() 
        {
            return await _powerStatusRepository.GetAll();
        }
        public async Task<PowerStatus> GetById(string Id) 
        {
            return await _powerStatusRepository.GetById(Id);
        }
        public async Task Create(PowerStatus ps) 
        {
            if (string.IsNullOrEmpty(ps.Id))
            {
                ps.Id = Guid.NewGuid().ToString();
            }
            ps.UpdatedAt = DateTime.Now;
            await _powerStatusRepository.Create(ps);
        }
        public async Task Update(PowerStatus ps) 
        {
            ps.UpdatedAt = DateTime.Now;
            await _powerStatusRepository.Update(ps);
        }
        public async Task Delete(string id) 
        { 
            var ps = await _powerStatusRepository.GetById(id);
            if (ps != null)
            {
                await _powerStatusRepository.Delete(ps);
            }
        }
    }
}
