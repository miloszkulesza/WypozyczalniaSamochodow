namespace WypozyczalniaSamochodow.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WypozyczalniaSamochodow.DAL;

    public sealed class Configuration : DbMigrationsConfiguration<WypozyczalniaSamochodow.DAL.WypozyczeniaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WypozyczalniaSamochodow.DAL.WypozyczeniaContext";
        }

        protected override void Seed(WypozyczalniaSamochodow.DAL.WypozyczeniaContext context)
        {
            WypozyczeniaInitializer.SeedAutaData(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
