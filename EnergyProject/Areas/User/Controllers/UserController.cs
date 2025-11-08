using EnergyProject.Data;
using EnergyProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnergyProject.Areas.User.Controllers
{
    [Area("User")]
    public class UserController : Controller
    {
        ApplicationDbContext db;
        public UserController(ApplicationDbContext db_)
        {
            db = db_;
        }
        public IActionResult Update(int Id) 
        {
            Models.User CurrUser = db.Users.First(u => u.Id == Id);

            return View(CurrUser);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePost(Models.User CurrUser) 
        {
            // db.Users.Update(CurrUser);
            Models.User User = db.Users.First(u => u.Id == CurrUser.Id);
            User.Phone = CurrUser.Phone;
            User.FullName = CurrUser.FullName;
            User.Email = CurrUser.Email;
            await db.SaveChangesAsync();
            return RedirectToAction("Profile", "Home");
        }

    }
}
