using EnergyProject.Data;
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
        public HomeController(ApplicationDbContext db_)
        {
            db = db_;
        }
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Profile()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            EnergyProject.Models.User user1;
            user1 = db.Users.FirstOrDefault(u => u.Id == currentUserId);
            if (user1 == null)
            {
                return NotFound();
            }
            return View(user1);
        }
    }
}
