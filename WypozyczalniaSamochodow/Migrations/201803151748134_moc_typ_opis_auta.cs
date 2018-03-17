namespace WypozyczalniaSamochodow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moc_typ_opis_auta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auto", "Moc", c => c.Int(nullable: false));
            AddColumn("dbo.Auto", "TypSilnika", c => c.String(nullable: false));
            AddColumn("dbo.Auto", "Opis", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auto", "Opis");
            DropColumn("dbo.Auto", "TypSilnika");
            DropColumn("dbo.Auto", "Moc");
        }
    }
}
