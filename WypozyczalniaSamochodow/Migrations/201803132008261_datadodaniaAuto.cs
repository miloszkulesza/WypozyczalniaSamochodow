namespace WypozyczalniaSamochodow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datadodaniaAuto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auto", "DataDodania", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auto", "DataDodania");
        }
    }
}
