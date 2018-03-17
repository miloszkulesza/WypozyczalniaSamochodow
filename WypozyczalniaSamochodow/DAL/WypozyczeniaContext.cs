using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WypozyczalniaSamochodow.Models;

namespace WypozyczalniaSamochodow.DAL
{
    public class WypozyczeniaContext : IdentityDbContext<ApplicationUser>
    {
        public WypozyczeniaContext() :base("WypozyczeniaContext")
        {

        }

        static WypozyczeniaContext()
        {
            Database.SetInitializer<WypozyczeniaContext>(new WypozyczeniaInitializer());
        }

        public static WypozyczeniaContext Create()
        {
            return new WypozyczeniaContext();
        }

        public DbSet<Auto> Auta { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Wypozyczenie> Wypozyczenia { get; set; }
        public DbSet<PozycjaZamowienia> PozycjeZamowienia { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}