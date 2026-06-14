using EnergyProject.Application.Interfaces;
using EnergyProject.Common.Enums;
using EnergyProject.Infrastructure.Data;
using EnergyProject.Infrastructure.Interfaces;
using EnergyProject.ViewModels;
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
        public IActionResult Show(string Id, DateTimeFilter dateTimeFilter)
        {
            MeterReadingFilterViewModel meterReadingFilterViewModel = new MeterReadingFilterViewModel();
            meterReadingFilterViewModel.meterReadings = _meterReadingService.GetMeterReadings(Id, dateTimeFilter);
            meterReadingFilterViewModel.meterId = Id;
            meterReadingFilterViewModel.dateTimeFilter = dateTimeFilter;
            return View(meterReadingFilterViewModel);
        }
    }
}
