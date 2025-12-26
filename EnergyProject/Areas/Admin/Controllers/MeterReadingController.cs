using EnergyProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnergyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MeterReadingController : Controller
    {
        public ApplicationDbContext db;
        public MeterReadingController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Info(string Id)
        {
            var meter = db.Meters
                .Include(m => m.MeterReadings.OrderByDescending(r => r.CreatedAt))
                .FirstOrDefault(m => m.Id == Id);
            return View(meter);
        }
    }
}
