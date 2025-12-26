using EnergyProject.Data;
using EnergyProject.Models;
using EnergyProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            return View(cd);
        }

        private static bool IsLuhnValid(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber)) return false;

            for (int i = 0; i < cardNumber.Length; i++)
                if (cardNumber[i] < '0' || cardNumber[i] > '9')
                    return false;

            int sum = 0;
            bool alt = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int n = cardNumber[i] - '0';
                if (alt)
                {
                    n *= 2;
                    if (n > 9) n -= 9;
                }
                sum += n;
                alt = !alt;
            }

            return sum % 10 == 0;
        }

        [HttpPost]
        public IActionResult CreatePost(CardDataCreateViewModel cd)
        {
            if (!IsLuhnValid(cd.CardNumber.ToString()))
                ModelState.AddModelError(string.Empty, "Invalid card number");

            var now = DateTime.UtcNow;
            if (cd.ExpYear < now.Year || (cd.ExpYear == now.Year && cd.ExpMonth < now.Month))
                ModelState.AddModelError(string.Empty, "Card is expired");

            if (string.IsNullOrWhiteSpace(cd.City) ||
                   string.IsNullOrWhiteSpace(cd.Street) ||
                   string.IsNullOrWhiteSpace(cd.House))
            {
                ModelState.AddModelError(string.Empty, "Please choose an address or fill in City, Street and House");
            }

            bool exists = db.Addresses.Any(x =>
                x.City == cd.City &&
                x.Street == cd.Street &&
                x.House == cd.House &&
                x.Apartment == cd.Apartment
            );

            CardData Card = new CardData();
            Card.Id = Guid.NewGuid().ToString();
            Card.IsDefault = false;
            Card.ExpMonth = cd.ExpMonth;
            Card.ExpYear = cd.ExpYear;
            Card.CardNumber = cd.CardNumber;
            Card.CardName = cd.CardName;
            Card.UserId = "1"; // todo

            if (exists)
            {
                /*TempData["Error"] = "This address already exists, try something differend";
                return RedirectToAction(nameof(Create));*/
                cd.AddressId = db.Addresses.FirstOrDefault(x =>
                    x.City == cd.City &&
                    x.Street == cd.Street &&
                    x.House == cd.House &&
                    x.Apartment == cd.Apartment
                ).Id;
            }
            else 
            {
                Address a = new Address();
                a.Apartment = cd.Apartment;
                a.City = cd.City;
                a.Street = cd.Street;
                a.House = cd.House;
                a.Id = Guid.NewGuid().ToString();
                cd.AddressId = a.Id;
                db.Addresses.Add(a);

            }

            Card.AddressId = cd.AddressId;

            if (!ModelState.IsValid)
                return View("Create", cd);

            /*if (string.IsNullOrWhiteSpace(cd.AddressId))
            {
                if (string.IsNullOrWhiteSpace(cd.City) ||
                    string.IsNullOrWhiteSpace(cd.Street) ||
                    string.IsNullOrWhiteSpace(cd.House))
                {
                    ModelState.AddModelError(string.Empty, "Please choose an address or fill in City, Street and House");
                }
                else 
                {
                    cd.AddressId = Guid.NewGuid().ToString();
                }
            }

            if (!ModelState.IsValid)
                return View("Create", cd);

           */


            db.CardDatas.Add(Card);
            db.SaveChanges();
            return RedirectToAction("Show");
        }

        
    }
}
