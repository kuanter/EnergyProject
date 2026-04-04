using EnergyProject.Infrastructure.Data;
using EnergyProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Security.Claims;

namespace EnergyProject.Areas.Client.Controllers 
{
    [Area("Client")]
    [Authorize(Policy = "ClientOnly")]
    public class HomeController : Controller
    {

        ApplicationDbContext db;
        private readonly ILogger _logger;
        public HomeController(ApplicationDbContext db_, ILogger<HomeController> logger)
        {
            db = db_;
            _logger = logger;
        }
        public IActionResult Home()
        {
            _logger.LogInformation("Used HomeClientController");
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Used PrivacyClientController");
            return View();
        }
        public IActionResult Profile()
        {
            _logger.LogInformation("Used ProfileClientController");
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            EnergyProject.Models.User user1;
            user1 = db.Users.FirstOrDefault(u => u.Id == currentUserId);
            
            _logger.LogInformation("Get user");

            if (user1 == null)
            {
                _logger.LogInformation("user is null");
                return NotFound();
            }
            return View(user1);
        }
    }
}
