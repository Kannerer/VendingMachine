using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace VendingMachine.Models
{
    public class ModelContext : DbContext
    {
        public DbSet<DrinkModel> DrinkModel { get; set; }
        public DbSet<CoinModel> CoinModel { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ModelContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}