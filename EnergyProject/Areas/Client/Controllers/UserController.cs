using EnergyProject.Data;
using EnergyProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnergyProject.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Policy = "ClientOnly")]
    public class UserController : Controller
    {
        ApplicationDbContext db;
        public UserController(ApplicationDbContext db_)
        {
            db = db_;
        }
        public IActionResult Update(string Id) 
        {
            Models.User CurrUser = db.Users.First(u => u.Id == Id);

            return View(CurrUser);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePost(Models.User CurrUser) 
        {
            // db.Users.Update(CurrUser);
            Models.User User = db.Users.First(u => u.Id == CurrUser.Id);
            User.PhoneNumber = CurrUser.PhoneNumber;
            User.UserName = CurrUser.UserName;
            User.Email = CurrUser.Email;
            await db.SaveChangesAsync();
            return RedirectToAction("Profile", "Home");
        }

    }
}
