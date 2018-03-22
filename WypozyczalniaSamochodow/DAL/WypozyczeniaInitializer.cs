using System;
using System.Collections.Generic;
using System.Data.Entity;
using WypozyczalniaSamochodow.Models;
using WypozyczalniaSamochodow.Migrations;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace WypozyczalniaSamochodow.DAL
{
    public class WypozyczeniaInitializer : MigrateDatabaseToLatestVersion<WypozyczeniaContext, Configuration>
    {
        public static void SeedAutaData(WypozyczeniaContext context)
        {
           
            var kategorie = new List<Kategoria>
            {
                new Kategoria() { KategoriaId = 1, NazwaKategorii = "Limuzyna" },
                new Kategoria() { KategoriaId = 2, NazwaKategorii = "Kompakt" },
                new Kategoria() { KategoriaId = 3, NazwaKategorii = "Miejski" },
                new Kategoria() { KategoriaId = 4, NazwaKategorii = "SUV" }
            };
            kategorie.ForEach(k => context.Kategorie.AddOrUpdate(k));
            context.SaveChanges();
            var auta = new List<Auto>()
            {
                new Auto() { AutoId = 1, Marka = "Opel", Model = "Astra", RokProdukcji = "2000", Silnik = "1.7", Cena = 20, Wypozyczony = false, KategoriaId = 2, DataDodania = new DateTime(2018, 03, 10), Moc = 68, TypSilnika = "Diesel",
                Opis = "Sprzedaż Astry II (zgodnie z kolejnością alfabetyczną zwanej Astrą G) rozpoczęła się w 1998 roku. " +
                "Od swojego poprzednika model odróżniał się nowocześniejszą, masywniejszą sylwetką i większymi rozmiarami, co " +
                "wpłynęło na przestronność wnętrza. Astra II jako pierwsza dostępna była również w wersji coupe, która podobnie" +
                " jak kabriolet została zaprojektowana przez stylistów Bertone. W roku 2004, gdy na rynku pojawiła się Astra III," +
                " produkcja niezmienionej Astry II (pod nazwą Astra Classic II) została przeniesiona do fabryki w Gliwicach." },
                new Auto() { AutoId = 2, Marka = "Ford", Model = "Fiesta", RokProdukcji = "2003", Silnik = "1.3", Cena = 30, Wypozyczony = false, KategoriaId = 3, DataDodania = new DateTime(2018, 03, 9), Moc = 70, TypSilnika = "Benzynowy",
                Opis = "Szósta generacja Fiesty produkowana w latach 2002-2008. Podobnie, jak jego poprzednik, " +
                "stylistycznie nawiązuje do linii Focusa. W roku 2005 Fiesta VI została poddana face-liftingowi, " +
                "który objął m. in. przednie i tylne światła, zderzaki oraz wnętrze. Auto wzbogaciło się również " +
                "o nowe elementy wyposażenia. Na bazie Fiesty w 2002 roku powstał model Fusion – połączenie " +
                "samochodu uteronowionego i kompaktowego vana. W Brazylii, Indiach, Chinach i RPA model nadal " +
                "produkowany jest również w wersji sedan (pod nazwą Ikon lub po prostu Fiesta Sedan)." },
                new Auto() { AutoId = 3, Marka = "Nissan", Model = "Qashqai", RokProdukcji = "2010", Silnik = "2.0", Cena = 50, Wypozyczony = false, KategoriaId = 4, DataDodania = new DateTime(2018, 03, 13), Moc = 150, TypSilnika = "Diesel",
                Opis = "Kompaktowy crossover SUV (segment I) produkowany od 2007 roku. W założeniu miał stanowić " +
                "połączenie miejskiej terenówki z autem segmentu C. W momencie debiutu Quashqai był najmłodszym " +
                "i najmniejszym SUV-em Nissana (dziś jest nim Juke). Nieoczekiwanie, model wyrósł na lidera w " +
                "swojej klasie z 2 milionami sprzedanych egzemplarzy. Jego poprzednikiem był mało znany w Europie" +
                " model Mistral. Od 2013 roku oferowana jest druga generacja modelu. Jego nazwa pochodzi od " +
                "Kaszkajów, plemienia nomadów zamieszkujących południowo-wschodni obszar Iranu." },
                new Auto() { AutoId = 4, Marka = "Audi", Model = "A4", RokProdukcji = "2005", Silnik = "1.9", Cena = 45, Wypozyczony = false, KategoriaId = 1, DataDodania = new DateTime(2018, 03, 10), Moc = 116, TypSilnika = "Diesel",
                    Opis = "Trzeci, obecnie produkowany model w rodzinie A4. Premiera samochodu " +
                    "oznaczonego symbolem B7 przypadła na rok 2004. Podobnie, jak miało to miejsce " +
                    "w przypadku A4 II, pod względem stylistycznym, najnowsza generacja modelu stanowi " +
                    "naturalną kontynuację linii swojego poprzednika, choć sylwetka auta znów została " +
                    "delikatnie unowocześniona (zwiększyły się też rozmiary). Tym razem nabrała ona " +
                    "również bardziej sportowego charakteru. Główną inspiracją dla projektantów A4 III " +
                    "był prototyp Nuvolari Quattro."}
            };
            auta.ForEach(a => context.Auta.AddOrUpdate(a));
            context.SaveChanges();
        }

        public static void SeedUzytkownicy(WypozyczeniaContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            const string name = "admin@wypozyczalnia.pl";
            const string password = "P@ssw0rd";
            const string roleName = "Admin";

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name, DaneKlienta = new DaneKlienta { Imie = "Jan", Nazwisko = "Kowalski", Adres = "Wesoła 5", Miasto = "Lublin", Telefon = "555444222" } };
                var result = userManager.Create(user, password);
            }
 
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}