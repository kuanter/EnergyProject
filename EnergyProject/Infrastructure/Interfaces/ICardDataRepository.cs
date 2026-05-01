using EnergyProject.Models;

namespace EnergyProject.Infrastructure.Interfaces
{
    public interface ICardDataRepository
    {
        public List<CardData> GetCardsByUserId(string userId);
    }
}
