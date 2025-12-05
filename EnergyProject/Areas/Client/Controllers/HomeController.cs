using System.Diagnostics;
using EnergyProject.Data;
using EnergyProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace EnergyProject.Areas.Client.Controllers 
{
    [Area("Client")]
    public class HomeController : Controller
    {
        //public IActionResult Accounts() => View();
        //public IActionResult Meters()     => View();
        //public IActionResult CreateBill() => View();

        ApplicationDbContext db;
        public HomeController(ApplicationDbContext db_)
        {
            db = db_;
        }
        public IActionResult Home()
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
        /*public IActionResult Readings() => View();
        public IActionResult Support() => View();
        public IActionResult Consumption() => View();
        public IActionResult Cards() => View();*/
    }
}
