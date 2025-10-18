using System.Diagnostics;
using EnergyProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnergyProject.Controllers;

public class  UserController : Controller
{
    public IActionResult Accounts() => View();
    public IActionResult Meters()     => View();
    public IActionResult CreateBill() => View();
    public IActionResult Home() => View();
    public IActionResult Profile() => View();
    public IActionResult Readings() => View();
    public IActionResult Support() => View();
    public IActionResult Consumption() => View();
    public IActionResult Cards() => View();
}