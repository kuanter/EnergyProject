using EnergyProject.Application.Interfaces;
using EnergyProject.Application.Interfaces.Stuff;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Models;

namespace EnergyProject.Application.Services
{
    public class CardDataService : ICardDataService
    {
        private readonly ICardDataRepository _cardDataRepository;
        private readonly ICurrentUserService _userService;
        private readonly IRepository<CardData> _repository;
        public CardDataService(ICardDataRepository cardDataRepository, ICurrentUserService userService, IRepository<CardData> repository)
        {
            _cardDataRepository = cardDataRepository;
            _userService = userService;
            _repository = repository;
        }
        public List<CardData> GetByCurrUser() {
            var userId = _userService.GetRequiredUserId();
            return _cardDataRepository.GetCardsByUserId(userId);
        }
        public void Delete(string id) { 
            
        }
    }
}
