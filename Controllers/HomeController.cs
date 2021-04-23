using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VendingMachine.Models;

namespace VendingMachine.Controllers
{
    public class HomeController : Controller
    {
        DrinkModelContext Context = new DrinkModelContext();
        public ActionResult Index()
        {
            return View(Context.DrinkModel);
        }
        public ActionResult AdminPanel()
        {
            return View(Context.DrinkModel);
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
        public ActionResult UpdateDrink(int drink_id, string new_name, int new_price, int new_count, bool new_available, string new_image)
        {
            var drink = Context.DrinkModel.Where(d => d.Id == drink_id).FirstOrDefault();

            Context.SaveChanges();
            return Json(drink);
        }
    }
}