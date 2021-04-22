using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace VendingMachine.Models
{
    public class DrinkModelContext : DbContext
    {
        public DbSet<DrinkModel> DrinkModel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DrinkModelContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}