using EnergyProject.Data;
using Microsoft.AspNetCore.Mvc;

namespace EnergyProject.Areas.Client.Controllers
{
    [Area("Client")]
    public class CardDataController : Controller
    {

        ApplicationDbContext db;
        public CardDataController(ApplicationDbContext db_)
        {
            db = db_;
        }
        public IActionResult Show()
        {
            var Cards = db.CardDatas;
            return View(Cards);
        }
    }
}
