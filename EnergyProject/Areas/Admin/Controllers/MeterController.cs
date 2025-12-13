using EnergyProject.Data;
using EnergyProject.Models;
using EnergyProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnergyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            var vm = new MeterCreateViewModel();
            vm.PaymentAccountOptions = db.PaymentAccounts
                .Where(p => p.MeterId == null)
                .Select(p =>
                new SelectListItem
                {
                    Value = p.Id,
                    Text = p.Id
                }).ToList();

           return View(vm);
        }
        [HttpPost]
        public IActionResult CreatePost(MeterCreateViewModel mcvm)
        {
            Meter m = new Meter();
            m.Id = Guid.NewGuid().ToString();
            m.SerialNumber = mcvm.SerialNumber;
            m.PaymentAccountId = mcvm.PaymentAccountId;
            m.InstallDate = DateTime.Now;
            m.IsActive = false;
            db.Meters.Add(m);
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
