using EnergyProject.Common.Models;
using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnergyProject.Infrastructure.Repositories
{
    public class CardDataRepository : Repository<CardData>, ICardDataRepository
    {
        public CardDataRepository(ApplicationDbContext db) : base(db) {}

        public List<CardData> GetListByUserId(string userId)
        {
            return _db.CardDatas.Where(c => c.UserId == userId).ToList();
        }

        public List<CardData> GetListByUserIdWithAddress(string userId)
        {
            return _db.CardDatas
                .Include(c => c.Address)
                .Where(c => c.UserId == userId)
                .ToList();
        }

        public async Task SetAsDefault(string cardId, string userId)
        {
            var cards = _db.CardDatas.Where(c => c.UserId == userId).ToList();
            foreach (var c in cards)
                c.IsDefault = false;

            var target = cards.FirstOrDefault(c => c.Id == cardId);

            if (target != null)
            {
                target.IsDefault = true;
                await _db.SaveChangesAsync();
            }
        }
    }
}
