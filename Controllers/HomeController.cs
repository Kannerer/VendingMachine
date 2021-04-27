using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendingMachine.Models;

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
        public ActionResult BuyDrink (int drink_id)
        {
            var drink = Context.DrinkModel.Where(d => d.Id== drink_id).FirstOrDefault();
            drink.Count--;
            if (drink.Count <= 0)
            {
                drink.Available = false;
            }
            Context.SaveChanges();
            return Json(drink);
        }

        [HttpPost]
        public ActionResult UpdateDrink(int drink_id, string new_name, int new_price, int new_count, bool new_available)
        {

            var drink = Context.DrinkModel.Where(d => d.Id == drink_id).FirstOrDefault();

            drink.Name = new_name;
            drink.Price = new_price;
            drink.Count = new_count;
            drink.Available = new_available;

            Context.SaveChanges();
            return Json("Drink updated successfully");
        }

        [HttpPost]
        public ActionResult DeleteDrink(int drink_id)
        {
            var drink = Context.DrinkModel.Where(d => d.Id == drink_id).FirstOrDefault();
            Context.DrinkModel.Remove(drink);
            Context.SaveChanges();
            return Json("Drink deleted");
        }
    }
}