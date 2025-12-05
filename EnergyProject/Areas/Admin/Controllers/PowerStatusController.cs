using EnergyProject.Data;
using EnergyProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnergyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PowerStatusController : Controller
    {
        public ApplicationDbContext db;
        public PowerStatusController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Show()
        {
            var PowerStatuses = db.PowerStatuses.ToList();
            return View(PowerStatuses);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreatePost(PowerStatus ps)
        {
            ps.Id = Guid.NewGuid().ToString();
            ps.UpdatedAt = DateTime.Now;
            db.PowerStatuses.Add(ps);
            db.SaveChanges();
            return RedirectToAction("Show");
        }
        public IActionResult Delete(string id)
        {
            var ps = db.PowerStatuses.Find(id);
            db.PowerStatuses.Remove(ps);
            db.SaveChanges();
            return RedirectToAction("Show");
        }
        public IActionResult Update(string Id) 
        {
            var ps = db.PowerStatuses.Find(Id);
            if (ps != null)
            {
                return View(ps);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult UpdatePost(PowerStatus ps) 
        {
            ps.UpdatedAt = DateTime.Now;
            db.PowerStatuses.Update(ps);
            db.SaveChanges();
            return RedirectToAction("Show");
        }
    }
}
