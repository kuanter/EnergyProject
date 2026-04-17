using EnergyProject.Application.Interfaces;
using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnergyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class MeterReadingController : Controller
    {
        public IMeterReadingService meterReadingService;
        public MeterReadingController(IMeterReadingService _meterReadingService)
        {
            meterReadingService = _meterReadingService;
        }
        
        private void AddReadingOnInfo(string id) // AddReading RENAME 
        {
            meterReadingService.AddReadingOnInfo(id);
        }

        public IActionResult Info(string Id)
        {
           
            AddReadingOnInfo(Id);

            var meterReadings = db.Meters
                .Include(m => m.MeterReadings.OrderByDescending(r => r.CreatedAt))
                .FirstOrDefault(m => m.Id == Id);

            return View(meterReadings);
        }
    }
}
