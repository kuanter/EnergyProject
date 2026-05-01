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
        public IMeterReadingService _meterReadingService;
        public MeterReadingController(IMeterReadingService meterReadingService)
        {
            _meterReadingService = meterReadingService;
        }

        public IActionResult Show(string Id)
        {

            return View(_meterReadingService.GetMeterReadings(Id));
        }
    }
}
