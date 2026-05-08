using EnergyProject.Application.Interfaces;
using EnergyProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnergyProject.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Policy = "ClientOnly")]
    public class CardDataController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICardDataService _cardDataService;

        public CardDataController(ILogger<HomeController> logger, ICardDataService cardDataService)
        {
            _logger = logger;
            _cardDataService = cardDataService;
        }

        public IActionResult Show()
        {
            _logger.LogInformation("Used ShowCardDataController");
            return View(_cardDataService.GetListByCurrUser());
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _cardDataService.Delete(id);
            return RedirectToAction(nameof(Show));
        }

        public async Task<IActionResult> SetAsDefault(string id)
        {
            _logger.LogInformation("Used SetAsDefaultCardDataController");
            await _cardDataService.SetAsDefault(id);
            return RedirectToAction(nameof(Show));
        }

        public IActionResult Create()
        {
            _logger.LogInformation("Used CreateCardDataController");
            return View(new CardDataCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CardDataCreateViewModel model)
        {
            _logger.LogInformation("Used CreatePostCardDataController");

            ModelState.Remove(nameof(model.AddressId));

            if (!ModelState.IsValid)
            {
                _logger.LogInformation("ModelState is invalid");
                return View("Create", model);
            }

            var result = await _cardDataService.CreateAsync(model);

            if (!result.Succeeded)
            {
                _logger.LogInformation("Card creation failed: " + result.ErrorMessage);
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View("Create", model);
            }

            _logger.LogInformation("Card created successfully");
            return RedirectToAction(nameof(Show));
        }
    }
}
