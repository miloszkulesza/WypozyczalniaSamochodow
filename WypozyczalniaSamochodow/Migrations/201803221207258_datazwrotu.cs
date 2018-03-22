namespace WypozyczalniaSamochodow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datazwrotu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Wypozyczenie", "iloscDni", c => c.Int(nullable: false));
            AddColumn("dbo.Wypozyczenie", "DataZwrotu", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Wypozyczenie", "DataZwrotu");
            DropColumn("dbo.Wypozyczenie", "iloscDni");
        }
    }
}
