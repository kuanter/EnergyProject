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
        
    }
}
