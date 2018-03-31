namespace WypozyczalniaSamochodow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredOpis : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Auto", "Opis", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Auto", "Opis", c => c.String());
        }
    }
}
