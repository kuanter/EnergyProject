using EnergyProject.Data;
using EnergyProject.Models;
using EnergyProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnergyProject.Areas.Admin.Controllers
{
    public class MeterController : Controller
    {
        public ApplicationDbContext db;
        public MeterController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Show()
        {
            var Meters = db.Meters.ToList();
            return View(Meters);
        }
        public IActionResult Create()
        {
            //var vm = new MeterCreateViewModel();
            //var paymentAccounts = db.PaymentAccounts.Where(p => p.MeterId == null).ToList();
            /** new SelectListItem
             {
                 Value = pa.Id,
                 Text = pa.Id
             }*/
            return View();
        }
        [HttpPost]
        /*public IActionResult CreatePost(Meter meter)
        {
            /*meter.Id = Guid.NewGuid().ToString();
            ps.UpdatedAt = DateTime.Now;
            db.PowerStatuses.Add(ps);
            db.SaveChanges();
            return RedirectToAction("Show");
        }*/
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
