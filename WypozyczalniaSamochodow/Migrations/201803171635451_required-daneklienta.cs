namespace WypozyczalniaSamochodow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requireddaneklienta : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "DaneKlienta_Telefon", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "DaneKlienta_Telefon", c => c.String());
        }
    }
}
