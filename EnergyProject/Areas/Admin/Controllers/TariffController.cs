using EnergyProject.Data;
using EnergyProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        public IActionResult Delete(string id)
        {
            var tariff = db.Tariffs.Find(id);
            if (tariff != null)
            {
                db.Tariffs.Remove(tariff);
                db.SaveChanges();
                return RedirectToAction("Show");
            }
            return RedirectToAction("Show");
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult CreatePost(Tariff t) 
        {
            t.Id = Guid.NewGuid().ToString();
            db.Tariffs.Add(t);
            db.SaveChanges();
            return RedirectToAction("Show");
        }
    }
}   


