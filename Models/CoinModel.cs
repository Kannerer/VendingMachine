using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VendingMachine.Models
{
    public class CoinModel
    {
        [Key]
        public int Nomination { get; set; }
        public int Count { get; set; }
        public bool Available { get; set; }
    }
}