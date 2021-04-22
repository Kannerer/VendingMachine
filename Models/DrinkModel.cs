using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VendingMachine.Models
{
    public class DrinkModel
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool Available { get; set; }
    }
}