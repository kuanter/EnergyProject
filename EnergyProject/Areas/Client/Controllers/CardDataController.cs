using EnergyProject.Data;
using EnergyProject.Models;
using EnergyProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult Delete(string id)
        {
            var cd = db.CardDatas.Find(id);
            db.CardDatas.Remove(cd);
            db.SaveChanges();
            return RedirectToAction("Show");
        }
        public IActionResult Create()
        {
            CardDataCreateViewModel cd = new CardDataCreateViewModel();
            cd.Addresses = db.Addresses.Select(pa => new SelectListItem
            {
                Value = pa.Id.ToString(),
                Text = pa.Id.ToString()
            }).ToList();
            return View(cd);
        }

        [HttpPost]
        public IActionResult CreatePost(CardDataCreateViewModel cd)
        {
            CardData Card = new CardData();
            Card.Id = Guid.NewGuid().ToString();
            Card.IsDefault = false;
            Card.ExpMonth = cd.ExpMonth;
            Card.CardNumber = cd.CardNumber;
            Card.CardName = cd.CardName;
            Card.UserId = "1"; // todo
            if (cd.AddressId == null)
            {
                Address a = new Address();
                a.Apartment = cd.Apartment;
                a.City = cd.City;
                a.Street = cd.Street;
                a.House = cd.House;
                a.Id = Guid.NewGuid().ToString();
                db.Addresses.Add(a);
                Card.AddressId = a.Id;
            }
            else {
                Card.AddressId = cd.AddressId;
            }
            
            db.CardDatas.Add(Card);
            db.SaveChanges();
            return RedirectToAction("Show");
        }

        
    }
}
