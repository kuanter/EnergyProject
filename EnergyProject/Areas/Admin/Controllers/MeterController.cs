using EnergyProject.Application.Interfaces;
using EnergyProject.Infrastructure.Data;
using EnergyProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EnergyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class MeterController : Controller
    {
        private IMeterService _meterService;
        private readonly ApplicationDbContext db;
        public MeterController(ApplicationDbContext context, IMeterService meterService)
        {
            db = context;
            _meterService = meterService;
        }
        public async Task<IActionResult> Show()
        {

            return View(await _meterService.Show());
        }

        public IActionResult Create()
        {
            var vm = new MeterCreateViewModel();
            vm.PaymentAccountOptions = db.PaymentAccounts.IgnoreQueryFilters()
                .Include(p => p.Address)
                .Where(p => !db.Meters.Any(m => m.PaymentAccountId == p.Id))
                .Select(p =>
                new SelectListItem
                {
                    Value = p.Id,
                    Text = $"city {p.Address.City}, str. {p.Address.Street}, house {p.Address.House}, ap. {p.Address.Apartment}"
                }).ToList();

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost(MeterCreateViewModel mcvm)
        {
            await _meterService.Create(mcvm);
            return RedirectToAction("Show");
        }

        public async Task<IActionResult> SwitchMeterStatus(string Id)
        {
            await _meterService.SwitchMeterStatus(Id);
            return RedirectToAction("Show");
        }
    }
}