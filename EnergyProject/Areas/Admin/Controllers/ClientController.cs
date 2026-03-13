using EnergyProject.Data;
using EnergyProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnergyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class ClientController : Controller
    {
        public ApplicationDbContext db;
        public ClientController(ApplicationDbContext db_)
        {
            db = db_;

        }
        public IActionResult Show(string? q)
        {
            var users = db.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.Trim();

                users = users.Where(u =>
                    EF.Functions.Like(u.UserName, "%" + q + "%") ||
                    EF.Functions.Like(u.Email, "%" + q + "%") ||
                    EF.Functions.Like(u.PhoneNumber, "%" + q + "%")
                );
            }

            var list = users.ToList();
            return View(list);
        }
     
    }

}
