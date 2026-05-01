using EnergyProject.Models;
using EnergyProject.ViewModels;

public interface IConsumptionService
{
    public Task<ConsumptionViewModel> GetConsumptionViewModel(string paymentAccountId, List<Bill> bills);
}

