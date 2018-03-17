namespace WypozyczalniaSamochodow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auto",
                c => new
                    {
                        AutoId = c.Int(nullable: false, identity: true),
                        KategoriaId = c.Int(nullable: false),
                        Marka = c.String(nullable: false),
                        Model = c.String(nullable: false),
                        Silnik = c.String(nullable: false),
                        RokProdukcji = c.String(nullable: false),
                        Wypozyczony = c.Boolean(nullable: false),
                        Cena = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.AutoId)
                .ForeignKey("dbo.Kategoria", t => t.KategoriaId, cascadeDelete: true)
                .Index(t => t.KategoriaId);
            
            CreateTable(
                "dbo.Kategoria",
                c => new
                    {
                        KategoriaId = c.Int(nullable: false, identity: true),
                        NazwaKategorii = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.KategoriaId);
            
            CreateTable(
                "dbo.PozycjaZamowienia",
                c => new
                    {
                        PozycjaZamowieniaId = c.Int(nullable: false, identity: true),
                        AutoId = c.Int(nullable: false),
                        WypozyczenieId = c.Int(nullable: false),
                        WartoscZamowienia = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PozycjaZamowieniaId)
                .ForeignKey("dbo.Auto", t => t.AutoId, cascadeDelete: true)
                .ForeignKey("dbo.Wypozyczenie", t => t.WypozyczenieId, cascadeDelete: true)
                .Index(t => t.AutoId)
                .Index(t => t.WypozyczenieId);
            
            CreateTable(
                "dbo.Wypozyczenie",
                c => new
                    {
                        WypozyczenieId = c.Int(nullable: false, identity: true),
                        Imie = c.String(nullable: false),
                        Nazwisko = c.String(nullable: false),
                        Adres = c.String(nullable: false),
                        Miasto = c.String(nullable: false),
                        Telefon = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        DataZlozenia = c.DateTime(nullable: false),
                        Wartosc = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.WypozyczenieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PozycjaZamowienia", "WypozyczenieId", "dbo.Wypozyczenie");
            DropForeignKey("dbo.PozycjaZamowienia", "AutoId", "dbo.Auto");
            DropForeignKey("dbo.Auto", "KategoriaId", "dbo.Kategoria");
            DropIndex("dbo.PozycjaZamowienia", new[] { "WypozyczenieId" });
            DropIndex("dbo.PozycjaZamowienia", new[] { "AutoId" });
            DropIndex("dbo.Auto", new[] { "KategoriaId" });
            DropTable("dbo.Wypozyczenie");
            DropTable("dbo.PozycjaZamowienia");
            DropTable("dbo.Kategoria");
            DropTable("dbo.Auto");
        }
    }
}
