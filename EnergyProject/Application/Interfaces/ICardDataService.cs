using EnergyProject.Models;

namespace EnergyProject.Application.Interfaces
{
    public interface ICardDataService
    {
        public List<CardData> GetByCurrUser();
        public void Delete(string id);
    }
}
