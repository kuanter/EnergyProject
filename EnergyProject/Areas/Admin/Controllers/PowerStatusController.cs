using EnergyProject.Application.Interfaces;
using EnergyProject.Infrastructure.Data;
using EnergyProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EnergyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class PowerStatusController : Controller
    {
        private IPowerStatusService _powerStatusService;
        public PowerStatusController(IPowerStatusService powerStatusService)
        {
            _powerStatusService = powerStatusService;
        }
        public async Task<IActionResult> Show()
        {
            return View(await _powerStatusService.Show());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost(PowerStatus ps)
        {
            await _powerStatusService.Create(ps);
            return RedirectToAction("Show");
        }
        public async Task<IActionResult> Delete(string id)
        {
            await _powerStatusService.Delete(id);
            return RedirectToAction("Show");
        }
        public async Task<IActionResult> Update(string Id) 
        {
            var powerStatus = await _powerStatusService.GetById(Id);
            if (powerStatus != null)
            {
                return View(powerStatus);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePost(PowerStatus ps) 
        {
            await _powerStatusService.Update(ps);
            return RedirectToAction("Show");
        }
    }
}
