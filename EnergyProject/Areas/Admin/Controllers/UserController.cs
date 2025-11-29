using EnergyProject.Data;
using Microsoft.AspNetCore.Mvc;
using EnergyProject.Models;

namespace EnergyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        public ApplicationDbContext db;
        public UserController(ApplicationDbContext db_)
        {
            db = db_;

        }
        public IActionResult Show()
        {
            var Users = db.Users.ToList();
            return View(Users);
        }
    }
}
