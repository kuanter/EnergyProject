using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Models;

namespace EnergyProject.Infrastructure.Repositories
{
    public class CardDataRepository : ICardDataRepository
    {
        ApplicationDbContext _db;
        public CardDataRepository(ApplicationDbContext db) {
            _db = db;
        }
        public List<CardData> GetCardsByUserId(string userId)
        {
            return _db.CardDatas.Where(c => c.UserId == userId).ToList();
        }
    }
}
