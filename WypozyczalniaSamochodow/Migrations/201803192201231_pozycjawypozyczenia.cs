namespace WypozyczalniaSamochodow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pozycjawypozyczenia : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PozycjaZamowienia", newName: "PozycjaWypozyczenia");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PozycjaWypozyczenia", newName: "PozycjaZamowienia");
        }
    }
}
