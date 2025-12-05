using EnergyProject.Data;
using EnergyProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnergyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TariffController : Controller
    {
        public ApplicationDbContext db;
        public TariffController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Show()
        {
            var Tarriffs = db.Tariffs.ToList();
            return View(Tarriffs);
        }
    }
}
