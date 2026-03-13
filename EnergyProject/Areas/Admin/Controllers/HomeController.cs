using EnergyProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EnergyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
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
