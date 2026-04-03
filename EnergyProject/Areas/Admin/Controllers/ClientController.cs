using EnergyProject.Application.Interfaces;
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
        private readonly IUserService _userService;
        public ClientController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Show(string? q)
        {
            var list = _userService.Show(q);
            return View(list);
        }
     
    }

}
