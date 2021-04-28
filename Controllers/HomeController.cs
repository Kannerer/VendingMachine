using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendingMachine.Models;
using Newtonsoft.Json;

namespace VendingMachine.Controllers
{
    public class HomeController : Controller
    {
        ModelContext Context = new ModelContext();
        public ActionResult Index()
        {
            ViewBag.Drink = Context.DrinkModel;
            ViewBag.Coin = Context.CoinModel;
            if (Request.QueryString["key"] == "42")
                return Redirect(Url.Action("AdminPanel"));
            else 
            return View();
        }
        public ActionResult AdminPanel()
        {
            ViewBag.Drink = Context.DrinkModel;
            ViewBag.Coin = Context.CoinModel;
            return View();
        }

        [HttpPost]
        public ActionResult BuyDrink (int drink_id, int sum)
        {
            var drink = Context.DrinkModel.Where(d => d.Id== drink_id).FirstOrDefault();
            if (sum >= drink.Price)
            {
                drink.Count--;
                if (drink.Count <= 0)
                {
                    drink.Available = false;
                }
                Context.SaveChanges();
            }

            return Json(drink);
        }

        [HttpPost]
        public ActionResult GetChange(int change)
        {
            var sum = 0;
            var coins = new Dictionary<int, int>(4);
            coins.Add(10, 0);
            coins.Add(5, 0);
            coins.Add(2, 0);
            coins.Add(1, 0);
            foreach (var coin in coins.Keys.ToList())
            {
                while (sum + coin <= change)
                {
                    sum += coin;
                    coins[coin] += 1;
                }
            }
            return Json(JsonConvert.SerializeObject(coins));
        }
            [HttpPost]
        public ActionResult UpdateDrink(int drink_id, string new_name, int new_price, int new_count, bool new_available)
        {

            var drink = Context.DrinkModel.Where(d => d.Id == drink_id).FirstOrDefault();
            if (new_name != "" && new_price > 0 && new_count >= 0)
            {
                drink.Name = new_name;
                drink.Price = new_price;
                drink.Count = new_count;
                if (new_count == 0)
                {
                    drink.Available = false;
                }
                else
                {
                    drink.Available = new_available;
                }
                Context.SaveChanges();
                return Json("Drink updated");
            }
            return Json(new { status = "error", message = "Указаны некорректные данные" });
        }

        [HttpPost]
        public ActionResult DeleteDrink(int drink_id)
        {
            var drink = Context.DrinkModel.Where(d => d.Id == drink_id).FirstOrDefault();
            Context.DrinkModel.Remove(drink);
            Context.SaveChanges();
            return Json("Drink deleted");
        }

        [HttpPost]
        public ActionResult UpdateCoin(int nomination, bool new_available)
        {
            var coin = Context.CoinModel.Where(c => c.Nomination == nomination).FirstOrDefault();
            coin.Available = new_available;
            Context.SaveChanges();
            return Json("Coin updated");
        }

        public ActionResult Drinks(string new_name, int new_price, int new_count, bool new_available)
        {
            if (new_name != "" && new_price > 0 && new_count >= 0)
            {
                var drink = new DrinkModel { Name = new_name, Price = new_price, Count = new_count, Available = new_available };
                Context.DrinkModel.Add(drink);
                Context.SaveChanges();
                ViewBag.item = drink;
                return PartialView("_Drinks", ViewBag.item);
            }
            return Json(new { status = "error", message = "Указаны некорректные данные" });
        }
    }
}