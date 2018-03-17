namespace WypozyczalniaSamochodow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usunieemaildaneklienta : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "DaneKlienta_Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "DaneKlienta_Email", c => c.String());
        }
    }
}
