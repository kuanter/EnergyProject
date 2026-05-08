using EnergyProject.Models;

namespace EnergyProject.Infrastructure.Interfaces
{
    public interface ICardDataRepository : IRepository<CardData>
    {
        public List<CardData> GetListByUserId(string userId);
        public List<CardData> GetListByUserIdWithAddress(string userId);
        public Task SetAsDefault(string cardId, string userId);
    }
}
