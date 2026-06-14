using EnergyProject.Common.Models;
using EnergyProject.ViewModels;

namespace EnergyProject.Application.Interfaces.Stuff
{
    public interface IConsumptionService
    {
        public Task<ConsumptionViewModel> GetConsumptionViewModel(string paymentAccountId, List<Bill> bills);
    }
}
