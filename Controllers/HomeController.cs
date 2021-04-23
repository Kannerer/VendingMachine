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
        public ActionResult UpdateDrink(int drink_id, string new_name, int new_price, int new_count, bool new_available)
        {
            var drink = Context.DrinkModel.Where(d => d.Id == drink_id).FirstOrDefault();
            
            drink.Name = new_name;
            drink.Price = new_price;
            drink.Count = new_count;
            drink.Available = new_available;

            Context.SaveChanges();
            return Json(drink);
        }

        [HttpPost]
        public ActionResult UpdateDrinkImage()
        {
            HttpPostedFileBase file = Request.Files[0];
            string fname = file.FileName;
            fname = Path.Combine(Server.MapPath("~/Images/"), fname);
            file.SaveAs(fname);

            return Json("File Uploaded Successfully!");

        }
    }
}