using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WypozyczalniaSamochodow.DAL;
using WypozyczalniaSamochodow.Models;

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

        public ActionResult Wypozyczenia()
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.UserIsAdmin = isAdmin;
            IEnumerable<Wypozyczenie> wypozyczenia;
            if(isAdmin)
            {
                wypozyczenia = db.Wypozyczenia.Include("PozycjaWypozyczenia").OrderByDescending(o => o.DataZlozenia).ToArray();
            }
            else
            {
                var userEmail = User.Identity.GetUserName();
                wypozyczenia = db.Wypozyczenia.Where(o => o.Email == userEmail).Include("PozycjaWypozyczenia").OrderByDescending(o => o.DataZlozenia).ToArray();
            }
            return View(wypozyczenia);
        }

    }
}