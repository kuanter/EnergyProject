using EnergyProject.Infrastructure.Data;
using EnergyProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EnergyProject.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Policy = "ClientOnly")]
    public class UserController : Controller
    {
        ApplicationDbContext db;
        private readonly ILogger _logger;
        public UserController(ApplicationDbContext db_, ILogger<HomeController> logger)
        {
            db = db_;
            _logger = logger;
        }
        public IActionResult Update(string Id) 
        {
            _logger.LogInformation("Used UpdateUserController");
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Models.User CurrUser = db.Users.FirstOrDefault(u => u.Id == currentUserId);

            _logger.LogInformation("Get user");

            if (CurrUser == null)
            {
                _logger.LogInformation("user is null");
                return NotFound();
            }
            return View(CurrUser);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePost(Models.User CurrUser) 
        {
            // db.Users.Update(CurrUser);
            _logger.LogInformation("Used UpdatePostUserController");

            Models.User User = db.Users.First(u => u.Id == CurrUser.Id);

            _logger.LogInformation("Get user");

            User.PhoneNumber = CurrUser.PhoneNumber;
            User.UserName = CurrUser.UserName;
            User.Email = CurrUser.Email;

            _logger.LogInformation("Set user data");

            await db.SaveChangesAsync();

            _logger.LogInformation("Save user");

            return RedirectToAction("Profile", "Home");
        }

    }
}
