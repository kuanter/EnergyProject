using EnergyProject.Application.Interfaces;
using EnergyProject.Application.Interfaces.Stuff;
using EnergyProject.Common.Models;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.ViewModels;

namespace EnergyProject.Application.Services
{
    public class CardDataService : ICardDataService
    {
        private readonly ICardDataRepository _cardDataRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ICurrentUserService _userService;

        public CardDataService(
            ICardDataRepository cardDataRepository,
            IAddressRepository addressRepository,
            ICurrentUserService userService)
        {
            _cardDataRepository = cardDataRepository;
            _addressRepository = addressRepository;
            _userService = userService;
        }

        public List<CardData> GetListByCurrUser()
        {
            string userId = _userService.GetRequiredUserId();
            return _cardDataRepository.GetListByUserIdWithAddress(userId);
        }

        public async Task<(bool Succeeded, string ErrorMessage)> CreateAsync(CardDataCreateViewModel model)
        { 
            if (!IsLuhnValid(model.CardNumber.ToString()))
                return (false, "Invalid card number.");

            var now = DateTime.UtcNow;
            if (model.ExpYear < now.Year || (model.ExpYear == now.Year && model.ExpMonth < now.Month))
                return (false, "Card is expired.");

            if (string.IsNullOrWhiteSpace(model.City) ||
                string.IsNullOrWhiteSpace(model.Street) ||
                string.IsNullOrWhiteSpace(model.House))
                return (false, "Please fill in City, Street and House.");

            var address = await _addressRepository.GetByDetails(
                model.City, model.Street, model.House, model.Apartment);

            if (address == null)
            {
                address = new Address(model.City, model.Street, model.House, model.Apartment);
                await _addressRepository.Create(address);
            }

            string userId = _userService.GetRequiredUserId();

            var card = new CardData(
                model.CardNumber,
                model.ExpMonth,
                model.ExpYear,
                model.CardName,
                model.IsDefault,
                address.Id,
                userId);

            await _cardDataRepository.Create(card);
            return (true, string.Empty);
        }

        public async Task SetAsDefault(string cardId)
        {
            string userId = _userService.GetRequiredUserId();
            await _cardDataRepository.SetAsDefault(cardId, userId);
        }

        public async Task Delete(string cardId)
        {
            await _cardDataRepository.Delete(await _cardDataRepository.GetById(cardId));
        }

        private static bool IsLuhnValid(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber)) return false;
            foreach (char ch in cardNumber)
                if (ch < '0' || ch > '9') return false;

            int sum = 0;
            bool alt = false;
            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int n = cardNumber[i] - '0';
                if (alt) { n *= 2; if (n > 9) n -= 9; }
                sum += n;
                alt = !alt;
            }
            return sum % 10 == 0;
        }
    }
}
