using EnergyProject.Application.Interfaces;
using EnergyProject.Infrastructure.Data;
using EnergyProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EnergyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class TariffController : Controller
    {
        public ApplicationDbContext db;
        private ITariffService _tariffService;
        public TariffController(ApplicationDbContext context, ITariffService tariffService)
        {
            db = context;
            _tariffService = tariffService;
        }
        public async Task<IActionResult> Show()
        {
            return View(await _tariffService.Show());
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _tariffService.Delete(id);
            return RedirectToAction("Show");
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> CreatePost(Tariff t) 
        {
            await _tariffService.Create(t);
            return RedirectToAction("Show");
        }

        public async Task<IActionResult> Update(string Id)
        {
            var tariff = await _tariffService.GetById(Id);
            if (tariff != null)
            {
                return View(tariff);
            }
            return NotFound();
        }

        public async Task<IActionResult> UpdatePost(Tariff t)
        {
            await _tariffService.Update(t);
            return RedirectToAction("Show");
        }
    }
}   


