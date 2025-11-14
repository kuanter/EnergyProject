using EnergyProject.Data;
using Microsoft.AspNetCore.Mvc;

namespace EnergyProject.Areas.User.Controllers
{
    [Area("User")]
    public class PaymentAccountController : Controller
    {
        ApplicationDbContext db;
        public PaymentAccountController(ApplicationDbContext db_)
        {
            db = db_;
        }
        public IActionResult Show()
        {
            // to do
            var pa = db.PaymentAccounts.ToList();
            return View(pa);
            
        }
      
    }
}
