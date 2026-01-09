using Microsoft.AspNetCore.Mvc;

namespace EnergyProject.Areas.Admin.Controllers
{
    public class BillController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
