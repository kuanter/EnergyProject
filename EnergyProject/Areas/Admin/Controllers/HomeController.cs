using System.Diagnostics;
using EnergyProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnergyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        
        public IActionResult Dashboard()     => View();
        public IActionResult Details() => View();
        public IActionResult Meters()     => View();
        public IActionResult Addresses() => View();
        public IActionResult Tariffs() => View();
        // public IActionResult Index() => RedirectToAction(nameof(Users));
    }
}
