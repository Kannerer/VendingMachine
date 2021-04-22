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
    }
}