using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WypozyczalniaSamochodow.DAL;

namespace WypozyczalniaSamochodow.Controllers
{
    public class WypozyczController : Controller
    {
        private WypozyczeniaContext db = new WypozyczeniaContext();

        // GET: Wypozycz
        public ActionResult WyswietlKategorie()
        {
            var kategorie = db.Kategorie.ToList();
            return View(kategorie);
        }

        public ActionResult WyswietlAuta(string kategoria)
        {
            var auta = db.Auta.Where(a => (a.Kategoria.NazwaKategorii == kategoria) && !a.Wypozyczony).ToList();
            return View(auta);
        }

        public ActionResult KartaPojazdu(int autoid)
        {
            var auto = db.Auta.Where(a => a.AutoId == autoid).Single();
            return View(auto);
        }

        public ActionResult PotwierdzDane(int autoid, int klientid)
        {
            //do oprogramowania
            return View();
        }

        public ActionResult Wypozycz(int autoid, int klientid)
        {
            //do oprogramowania
            return View();
        }

    }
}