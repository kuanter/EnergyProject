using System.Diagnostics;
using EnergyProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnergyProject.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Users()     => View();
        public IActionResult Details() => View();
        public IActionResult Meters()     => View();
        public IActionResult Addresses() => View();
        public IActionResult Tariffs() => View();
        public IActionResult Index() => RedirectToAction(nameof(Users));
    }
}
