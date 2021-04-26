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

        public ActionResult Drinks()
        {
            return PartialView("_Drinks", Context.DrinkModel);
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
        public ActionResult UpdateDrink()
        {
            var id = Convert.ToInt32(Request["id"]);
            var drink = Context.DrinkModel.Where(d => d.Id == id).FirstOrDefault();
            
            drink.Name = Request["name"];
            drink.Price = Convert.ToInt32(Request["price"]);
            drink.Count = Convert.ToInt32(Request["count"]);
            drink.Available = Convert.ToBoolean(Request["available"]);
            if (Request.Files.AllKeys.Length != 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                string fname = file.FileName;
                fname = Path.Combine(Server.MapPath("~/Images/"), fname);
                file.SaveAs(fname);
            }
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