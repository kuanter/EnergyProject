using EnergyProject.Data;
using EnergyProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Diagnostics;

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
            EnergyProject.Models.User user1;
            user1 = db.Users.First();
            // To do
            return View(user1);
        }
    }
}
