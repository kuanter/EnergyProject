using EnergyProject.Models;
using EnergyProject.ViewModels;

namespace EnergyProject.Application.Interfaces
{
    public interface ICardDataService
    {
        public List<CardData> GetListByCurrUser();
        public Task<(bool Succeeded, string ErrorMessage)> CreateAsync(CardDataCreateViewModel model);
        public Task SetAsDefault(string cardId);
        public Task Delete(string id);
    }
}
